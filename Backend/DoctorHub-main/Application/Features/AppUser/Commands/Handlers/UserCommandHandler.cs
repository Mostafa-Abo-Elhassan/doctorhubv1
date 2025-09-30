using Application.Bases;
using Application.Enums;
using Application.Features.AppUser.Commands.Models;
using Application.Helpers;
using AutoMapper;
using Domain.AutheServices;
using Domain.Entities;
using Domain.Interfaces;
using Google.Apis.Auth;
using Infrastructure.Data;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Application.Features.AppUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<LoginRequestDto, Response<AuthResponseDto>>,
        IRequestHandler<RegisterRequestDto, Response<string>>,
        IRequestHandler<ForgotPasswordDto, Response<string>>,
         IRequestHandler<otp, Response<string>>,
        IRequestHandler<ResetPasswordDto, Response<string>>,
        //IRequestHandler<SocialLoginDto, Response<AuthResponseDto>>,
        IRequestHandler<ChangePasswordDto, Response<string>>,
        IRequestHandler<GoogleLoginDto, Response<AuthResponseDto>>

    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IautheService _iauthe;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtpService _OtpService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGoogleTokenValidator _googleValidator;

        //private readonly IHttpClientFactory _httpClientFactory;

        #endregion


        #region constractors
        public UserCommandHandler
            (
          IMapper mapper,
            IJwtService jwtService,
            IEmailService emailService,
            ApplicationDbContext context,
            IUnitOfWork unitOfWork,
            IautheService iauthe,
            IConfiguration configuration,
            //IHttpClientFactory httpClientFactory
            IOtpService OtpService,
            IHttpContextAccessor httpContextAccessor,
             IGoogleTokenValidator googleValidator

            )
        {
            _mapper = mapper;
            _jwtService = jwtService;
            _iauthe = iauthe;
            _emailService = emailService;
            _configuration = configuration;
            //_httpClientFactory = httpClientFactory;
            _unitOfWork = unitOfWork;
            _OtpService = OtpService;
            _httpContextAccessor = httpContextAccessor;

            _googleValidator = googleValidator;



        }

        #endregion


        // POST /api/auth/register
        public async Task<Response<string>> Handle(RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName,
                Role = request.Role,
                NationalId = request.NationalId,
                Email = $"{request.NationalId}@system.local",  // ✅ توليد ايميل وهمي

            };

            // 1) إنشاء الـ User
            var result = await _iauthe.CreateUserAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest<string>(string.Join(", ", errors));
            }
            //await _iauthe.AddToRoleAsync(user, UserRole.Patient);

            ////if (user==null)
            ////{
            //// 2) ربط Role + إنشاء Patient/Doctor
            if (string.IsNullOrWhiteSpace(request.Role) || request.Role.Equals(UserRole.Patient, StringComparison.OrdinalIgnoreCase))
            {
                var patient = new Patient
                {
                    UserId = user.Id
                };

                await _unitOfWork.PatientRepository.AddAsync(patient);
                await _unitOfWork.PatientRepository.SaveChangesAsync();

                await _iauthe.AddToRoleAsync(user, UserRole.Patient);
            }

            else if (request.Role == UserRole.Doctor)
            {
                var doctor = new Doctor
                {
                    UserId = user.Id,
                    LicenseNumber = request.LicenseNumber
                    //Speciality = request.Speciality
                };

                await _unitOfWork.DoctorRepository.AddAsync(doctor);
                await _unitOfWork.DoctorRepository.SaveChangesAsync();

                await _iauthe.AddToRoleAsync(user, UserRole.Doctor.ToString());
            }
            else
            {
                // أدوار أخرى لو موجودة
                await _iauthe.AddToRoleAsync(user, "Patient");
            }
            return Created<string>("User registered successfully");

            //}


            //return Created<string>("User registered successfully but not  seeding to your role");
        }


        public async Task<Response<AuthResponseDto>> Handle(LoginRequestDto request, CancellationToken cancellationToken)
        {
            // البحث بالرقم القومي
            var user = await _iauthe.FindUserByNationalIdAsync(request.NationalId);
            if (user == null)
                return BadRequest<AuthResponseDto>("Invalid National ID or password");

            var result = await _iauthe.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return BadRequest<AuthResponseDto>("Invalid National ID or password");

            // جلب الأدوار
            var userRoles = await _iauthe.GetRolesAsync(user);
            var role = userRoles.FirstOrDefault() ?? "Unknown";

            // توليد Tokens
            var (accessToken, refreshToken) = await _jwtService.GenerateTokensAsync(user);

            // ✅ جلب بيانات إضافية حسب الـ Role
            object? profileData = null;

            if (role == UserRole.Patient.ToString())
            {
                var patient = await _unitOfWork.PatientRepository
                    .GetWithCareria(p => p.UserId == user.Id, new[] { "Personal" });

                if (patient != null)
                {
                    profileData = new
                    {
                        patient.Id,
                        patient.UserId,
                        FullName = patient.Personal?.FullName,
                        patient.Personal?.NationalId,
                        patient.Personal?.DateOfBirth
                    };
                }
            }
            else if (role == UserRole.Doctor.ToString())
            {
                var doctor = await _unitOfWork.DoctorRepository
                    .GetWithCareria(p => p.UserId == user.Id, new[] { "Personal" });

                if (doctor != null)
                {
                    profileData = new
                    {
                        doctor.Id,
                        doctor.UserId,
                        FullName = doctor.Personal?.FullName,
                        doctor.Speciality,
                        doctor.LicenseNumber
                    };
                }
            }
            //else if (role == UserRole.Pharmacy.ToString())
            //{
            //    var pharmacy = await _unitOfWork.PharmacyRepository
            //        .GetWithCareria(ph => ph.UserId == user.Id, null, cancellationToken);

            //    if (pharmacy != null)
            //    {
            //        profileData = new
            //        {
            //            pharmacy.Id,
            //            pharmacy.UserId,
            //            pharmacy.Name,
            //            pharmacy.Address
            //        };
            //    }
            //}
            //else if (role == UserRole.Lab.ToString())
            //{
            //    var lab = await _unitOfWork.LabRepository
            //        .GetWithCareria(l => l.UserId == user.Id, null, cancellationToken);

            //    if (lab != null)
            //    {
            //        profileData = new
            //        {
            //            lab.Id,
            //            lab.UserId,
            //            lab.LabName,
            //            lab.LicenseNumber
            //        };
            //    }
            //}
            // ✨ تقدر تكمل باقي الـ roles (Hospital, HealthCenter...) بنفس الشكل

            // بناء الـ Response
            var response = new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Role = role,
                ExpiresAt = DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration.GetSection("JwtSettings")["ExpiryInMinutes"])),
                Profile = profileData
            };

            return Success(response);
        }




        public async Task<Response<string>> Handle(ForgotPasswordDto request, CancellationToken cancellationToken)
        {
            var user = await _iauthe.FindUserByEmailAsync(request.Email);
            if (user == null)
                return NotFound<string>("User not found");

            var otp = _OtpService.GenerateOtp(request.Email);

            var emailBody = $@"
       <div style='font-family: Arial, sans-serif; color: #333;'>
        <h2 style='color: #2E86C1;'>Hello from Trippool!</h2>
        <p>Thank you for using <strong>Trippool</strong>.</p>
        <p>Your One-Time Password (OTP) code is:</p>
        <h1 style='color: #E74C3C;'>{otp}</h1>
        <p>Please use this code to reset your password. This code is valid for the next 10 minutes.</p>
        <p>If you did not request a password reset, please ignore this email.</p>
        <hr style='border:none; border-top:1px solid #eee;'/>
        <p style='font-size: 12px; color: #999;'>Trippool Team<br/>support@trippool.com</p>
         </div>
        ";



            // Send Email – replace this with your email sender
            await _emailService.SendEmailAsync(user.Email, "OTP Code", emailBody);


            return Success($"OTP sent to your email :{otp}");
        }


        public async Task<Response<string>> Handle(otp request, CancellationToken cancellationToken)
        {
            bool isValid = _OtpService.ValidateOtp(request.Email, request.Otp);
            if (!isValid)
            {

                return BadRequest<string>("Invalid OTP");

            }

            return Success("OTP Verified");

        }


        // POST /api/auth/reset-password
        public async Task<Response<string>> Handle(ResetPasswordDto request, CancellationToken cancellationToken)
        {
            var user = await _iauthe.FindUserByEmailAsync(request.Email);
            if (user == null)
                return NotFound<string>("User not found");

            var result = await _iauthe.ResetPasswordAsync(user, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest<string>(string.Join(", ", errors));
            }

            return Success("Password reset successfully");
        }

        //// POST /api/auth/social-login
        //public async Task<Response<AuthResponseDto>> Handle(SocialLoginDto request, CancellationToken cancellationToken)
        //{
        //    User user;
        //    if (request.Provider.ToLower() == "google")
        //    {
        //        user = await ValidateGoogleToken(request.Token);
        //    }
        //    //else if (request.Provider.ToLower() == "facebook")
        //    //{
        //    //    user = await ValidateFacebookToken(request.Token);
        //    //}
        //    else
        //    {
        //        return BadRequest<AuthResponseDto>("Unsupported provider");
        //    }

        //    if (user == null)
        //        return BadRequest<AuthResponseDto>("Invalid social token");

        //    var token = await _jwtService.GenerateTokenAsync(user);
        //    var response = new AuthResponseDto
        //    {
        //        Token = token,
        //        Role = user.Role,
        //        ExpiresAt = DateTime.UtcNow.AddMinutes(
        //            int.Parse(_configuration.GetSection("JwtSettings")["ExpiryInMinutes"]))
        //    };

        //    return Success(response);
        //}

        public async Task<Response<string>> Handle(ChangePasswordDto request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();// 1- نجيب اليوزر من الـ JWT Claims
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>("User not authenticated");

            var user = await _iauthe.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound<string>("User not found");

            // 2- نغير الباسورد
            var result = await _iauthe.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest<string>(string.Join(", ", errors));
            }

            return Success("Password changed successfully");
        }

        public async Task<Response<AuthResponseDto>> Handle(GoogleLoginDto request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.IdToken))
                return BadRequest<AuthResponseDto>("IdToken is required.");

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await _googleValidator.ValidateAsync(request.IdToken);
            }
            catch (Exception)
            {
                return Unauthorized<AuthResponseDto>("Invalid Google token.");
            }

            var email = payload.Email;
            var name = payload.Name;
            var providerKey = payload.Subject; // ثابت من Google لكل يوزر  
            const string provider = "Google";

            // 1) جرّب تلاقي يوزر مربوط بـ Google مباشرة  
            var user = await _iauthe.FindUserByLoginAsync(provider, providerKey);

            // 2) لو مش موجود، دوري بالإيميل  
            if (user == null && !string.IsNullOrEmpty(email))
                user = await _iauthe.FindUserByEmailAsync(email);

            // 3) لو برضه مش موجود: أنشئ يوزر جديد  
            if (user == null)
            {
                user = new User
                {
                    UserName = !string.IsNullOrEmpty(email) ? email : $"google_{providerKey}",
                    Email = email,
                    EmailConfirmed = true, // غالباً Google بيديك Email Verified، فبنعلّمها Confirmed                                             //FullName = name // لو عندك حقل Name  
                    Role = UserRole.Patient.ToString() // أو أي دور تاني حسب منطقك

                };
                // ممكن تحط باسورد عشوائي، بس مش هيستخدمه
                var password = Guid.NewGuid().ToString("N") + "!Aa1";
                var createRes = await _iauthe.CreateUserAsync(user, password);
                if (!createRes.Succeeded)
                    return BadRequest<AuthResponseDto>(string.Join(", ", createRes.Errors.Select(e => e.Description)));


                await _iauthe.AddToRoleAsync(user, UserRole.Patient.ToString());

            }

            // 4) اربط اليوزر بالـ Google login لو مفيش ربط  
            var loginInfo = new UserLoginInfo(provider, providerKey, provider);
            var addLoginRes = await _iauthe.AddLoginAsync(user, loginInfo);
            // Ignore Duplicate login errors لو الربط موجود بالفعل  

            // 5) أصدِر JWT + Refresh (لو عندك)  
            var (jwt, refreshtoken) = await _jwtService.GenerateTokensAsync(user);
            var response = new AuthResponseDto
            {
                Token = jwt,
                ExpiresAt = DateTime.Now.AddHours(1), // حسب إعداداتك  
                Role = user.Role,
                RefreshToken = refreshtoken // Add refresh token logic if applicable  
            };

            return Success(response);

        }




        //    private async Task<User> ValidateGoogleToken(string token)
        //    {
        //        try
        //        {
        //            var settings = new GoogleJsonWebSignature.ValidationSettings
        //            {
        //                Audience = new[] { _configuration["GoogleSettings:ClientId"] }
        //            };

        //            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);

        //            // Find or create user
        //            var user = await _iauthe.FindUserByEmailAsync(payload.Email);
        //            if (user == null)
        //            {
        //                user = new User
        //                {
        //                    UserName = payload.Email,
        //                    Email = payload.Email,
        //                    Role = "Rider", // Default role, can be changed based on your logic
        //                    EmailConfirmed = true
        //                };

        //                var result = await _iauthe.CreateUserByObjectAsync(user);
        //                if (!result.Succeeded)
        //                    return null;

        //                await _iauthe.AddToRoleAsync(user, user.Role);
        //            }

        //            return user;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }


    }

}

