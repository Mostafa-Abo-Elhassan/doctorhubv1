using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Domain.AutheServices
{
    public class AutheService : IautheService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IUnitOfWork _unitOfWork;
        public AutheService(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager)

        {
            _userManager = userManager;
            _signInManager = signInManager;

            _unitOfWork = unitOfWork;

        }
        public async Task AddToRoleAsync(User user, string role) => await _userManager.AddToRoleAsync(user, role);


        public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool rememberme)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, rememberme);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);

        public async Task<IdentityResult> CreateUserByObjectAsync(User user) => await _userManager.CreateAsync(user);


        public async Task<User> FindUserByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);


        public async Task<string> GeneratePasswordResetTokenAsync(User user) => await _userManager.GeneratePasswordResetTokenAsync(user);


        public async Task<ICollection<string>> GetRolesAsync(User user) => await _userManager.GetRolesAsync(user);


        public async Task<User> GetUserByIdAsync(string id) => await _userManager.FindByIdAsync(id);


        public async Task<IdentityResult> ResetPasswordAsync(User user, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }


        public async Task<IdentityResult> UpdateAsync(User user) => await _userManager.UpdateAsync(user);

        public async Task<User> FindUserByLoginAsync(string loginProvider, string providerKey)
        {
            // هنا Identity بيخزن بيانات اللوجين الخارجي (Google/Facebook...) 
            return await _userManager.FindByLoginAsync(loginProvider, providerKey);
        }

        public async Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo login)
        {
            return await _userManager.AddLoginAsync(user, login);
        }

        public async Task<User?> FindUserByNationalIdAsync(string nationalId)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(u => u.NationalId == nationalId);
        }

    }
}
