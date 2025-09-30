//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Serilog;
//using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using TripPool.Core.Bases;
//using TripPool.Core.Features.Admin.Models;
//using TripPool.Core.Features.Admin.Queries;
//using TripPool.Data.Entities;
//using TripPool.Infrastructure.Data;
//using iText.Kernel.Geom;
//using iText.Kernel.Pdf;
//using iText.Layout.Element;
//using TripPool_Core.Bases;
//using TripPool_Core.Features.Admin.Queries.Models;
//using TripPool_Core.Features.Admin.Queries.Responses;
//using TripPool_Data.Entities;
//using TripPool_Inferastracture.Data;

//namespace TripPool.Core.Features.Admin.Handlers
//{
//    public class AdminQueryHandler : ResponseHandler,
//        IRequestHandler<GetDashboardQuery, Response<DashboardResponse>>,
//        IRequestHandler<GetUsersQuery, Response<List<UserResponse>>>,
//        IRequestHandler<GetUserDetailsQuery, Response<UserDetailsResponse>>,
//        IRequestHandler<GetPendingDriversQuery, Response<List<DriverResponse>>>,
//        IRequestHandler<GetDriverDetailsQuery, Response<DriverResponse>>,
//        IRequestHandler<GetTripsQuery, Response<List<TripResponse>>>,
//        IRequestHandler<GetTripDetailsQuery, Response<TripResponse>>,
//        IRequestHandler<GetUsageAnalyticsQuery, Response<UsageAnalyticsResponse>>,
//        IRequestHandler<GetDriverAnalyticsQuery, Response<DriverAnalyticsResponse>>,
//        IRequestHandler<GetVehicleAnalyticsQuery, Response<VehicleAnalyticsResponse>>,
//        IRequestHandler<GenerateReportQuery, Response<string>>
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly ApplicationDbContext _context;
//        private readonly ILogger _logger;


//        public AdminQueryHandler(UserManager<User> userManager, ApplicationDbContext context, ILogger logger)
//        {
//            _userManager = userManager;
//            _context = context;
//            _logger = logger;

//        }



//        public async Task<Response<DashboardResponse>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching dashboard metrics...", GetCurrentAdminId());

//            var users = await _userManager.Users.ToListAsync(cancellationToken);
//            var trips = await _context.Trips.ToListAsync(cancellationToken);
//            var drivers = await _userManager.GetUsersInRoleAsync("Driver");

//            var response = new DashboardResponse
//            {
//                TotalUsers = users.Count,
//                TotalRiders = users.Count(u => u.Role == "Rider"),
//                TotalDrivers = users.Count(u => u.Role == "Driver"),
//                TotalCompanies = users.Count(u => u.Role == "Company"),
//                TotalTrips = trips.Count,
//                CompletedTrips = trips.Count(t => t.Status == "Completed"),
//                OngoingTrips = trips.Count(t => t.Status == "Ongoing"),
//                CanceledTrips = trips.Count(t => t.Status == "Cancelled"),
//                TotalRevenue = trips.Where(t => t.Status == "Completed").Sum(t => t.FarePerRider),
//                PlatformCommission = trips.Where(t => t.Status == "Completed").Sum(t => t.Commission),
//                NetRevenue = trips.Where(t => t.Status == "Completed").Sum(t => t.FarePerRider - t.Commission),
//                ActiveDrivers = drivers.Count(d => d.IsApproved),
//                RecentActivities = await _context.AuditLogs
//                    .OrderByDescending(a => a.Timestamp)
//                    .Take(10)
//                    .Select(a => new RecentActivity { Description = a.Description, Timestamp = a.Timestamp })
//                    .ToListAsync(cancellationToken),
//                UserGrowth = await _context.Users
//                    .GroupBy(u => u.LockoutEnd.HasValue ? u.LockoutEnd.Value.Date : DateTime.UtcNow.Date)
//                    .Select(g => new UserGrowthData { Date = g.Key, UserCount = g.Count() })
//                    .ToListAsync(cancellationToken),
//                TripTrends = await _context.Trips
//                    .GroupBy(t => t.StartTime.Date)
//                    .Select(g => new TripTrendData { Date = g.Key, TripCount = g.Count() })
//                    .ToListAsync(cancellationToken),
//                RevenueTrends = await _context.Trips
//                    .Where(t => t.Status == "Completed")
//                    .GroupBy(t => t.EndTime.Date)
//                    .Select(g => new RevenueTrendData { Date = g.Key, Revenue = g.Sum(t => t.FarePerRider) })
//                    .ToListAsync(cancellationToken)
//            };

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetDashboard",
//                Description = $"Admin {GetCurrentAdminId()} accessed dashboard metrics",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Dashboard metrics retrieved successfully by admin {AdminId}", GetCurrentAdminId());
//            return Success(response, "Dashboard metrics retrieved successfully");
//        }

//        public async Task<Response<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching users with role {Role} and approval status {IsApproved}...",
//                GetCurrentAdminId(), request.Role ?? "Any", request.IsApproved?.ToString() ?? "Any");

//            var usersQuery = _userManager.Users.AsQueryable();
//            if (!string.IsNullOrEmpty(request.Role))
//                usersQuery = usersQuery.Where(u => u.Role == request.Role);
//            if (request.IsApproved.HasValue)
//                usersQuery = usersQuery.Where(u => u.IsApproved == request.IsApproved.Value);

//            var users = await usersQuery
//                .Select(u => new UserResponse
//                {
//                    Id = u.Id,
//                    Name = u.UserName,
//                    Email = u.Email,
//                    PhoneNumber = u.PhoneNumber,
//                    Role = u.Role,
//                    ProfilePhoto = u.ProfilePhoto,
//                    Rating = u.Rating
//                })
//                .ToListAsync(cancellationToken);

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetUsers",
//                Description = $"Admin {GetCurrentAdminId()} fetched users with role {request.Role ?? "Any"} and approval status {request.IsApproved?.ToString() ?? "Any"}",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched {UserCount} users", GetCurrentAdminId(), users.Count);
//            return Success(users, $"Successfully fetched {users.Count} users");
//        }

//        public async Task<Response<UserDetailsResponse>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching details for user {UserId}...", GetCurrentAdminId(), request.UserId);

//            var user = await _userManager.FindByIdAsync(request.UserId);
//            if (user == null)
//            {
//                _logger.Warning("User {UserId} not found for admin {AdminId}", request.UserId, GetCurrentAdminId());
//                return NotFound<UserDetailsResponse>("User not found");
//            }

//            var trips = await _context.RiderTrips
//                .Where(rt => rt.RiderID == request.UserId)
//                .Select(rt => new TripResponse
//                {
//                    TripId = rt.TripID,
//                    RiderId = rt.RiderID,
//                    DriverId = rt.Trip.DriverID,
//                    StartLocation = rt.Trip.StartLocation,
//                    EndLocation = rt.Trip.EndLocation,
//                    StartTime = rt.Trip.StartTime,
//                    EndTime = rt.Trip.EndTime,
//                    Status = rt.Trip.Status,
//                    Fare = rt.Fare
//                })
//                .ToListAsync(cancellationToken);

//            var reviewsWritten = await _context.Reviews
//                .Where(r => r.ReviewerID == request.UserId)
//                .Select(r => new ReviewResponse
//                {
//                    ReviewId = r.ReviewID,
//                    Rating = r.Rating,
//                    Comment = r.Comment,
//                    Timestamp = r.Timestamp
//                })
//                .ToListAsync(cancellationToken);

//            var reviewsReceived = await _context.Reviews
//                .Where(r => r.RevieweeID == request.UserId)
//                .Select(r => new ReviewResponse
//                {
//                    ReviewId = r.ReviewID,
//                    Rating = r.Rating,
//                    Comment = r.Comment,
//                    Timestamp = r.Timestamp
//                })
//                .ToListAsync(cancellationToken);

//            var response = new UserDetailsResponse
//            {
//                Id = user.Id,
//                Name = user.UserName,
//                Email = user.Email,
//                PhoneNumber = user.PhoneNumber,
//                Role = user.Role,
//                ProfilePhoto = user.ProfilePhoto,
//                Rating = user.Rating,
//                PaymentInfo = user.PaymentInfo,
//                CompanyId = user.CompanyID,
//                TripHistory = trips,
//                ReviewsWritten = reviewsWritten,
//                ReviewsReceived = reviewsReceived
//            };

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetUserDetails",
//                Description = $"Admin {GetCurrentAdminId()} fetched details for user {user.Id}",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched details for user {UserId}", GetCurrentAdminId(), user.Id);
//            return Success(response, "User details retrieved successfully");
//        }

//        public async Task<Response<List<DriverResponse>>> Handle(GetPendingDriversQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching pending drivers...", GetCurrentAdminId());

//            var drivers = await _userManager.GetUsersInRoleAsync("Driver");
//            var pendingDrivers = drivers
//                .Where(d => !d.IsApproved)
//                .Select(d => new DriverResponse
//                {
//                    Id = d.Id,
//                    Name = d.UserName,
//                    Email = d.Email,
//                    PhoneNumber = d.PhoneNumber,
//                    CompanyId = d.CompanyID,
//                    IsApproved = d.IsApproved,
//                    VehiclesDriven = d.VehiclesDriven?.Select(v => new VehicleResponse
//                    {
//                        VehicleId = v.VehicleID,
//                        Model = v.Model,
//                        Capacity = v.Capacity
//                    }).ToList()
//                })
//                .ToList();

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetPendingDrivers",
//                Description = $"Admin {GetCurrentAdminId()} fetched {pendingDrivers.Count} pending drivers",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            // Notify each pending driver (if they haven't been notified recently)
//            foreach (var driver in pendingDrivers)
//            {
//                var recentNotification = await _context.Notifications
//                    .Where(n => n.UserID == driver.Id && n.Message.Contains("Your driver application is pending approval"))
//                    .OrderByDescending(n => n.Timestamp)
//                    .FirstOrDefaultAsync(cancellationToken);

//                if (recentNotification == null || (DateTime.UtcNow - recentNotification.Timestamp).TotalDays > 1)
//                {
//                    await _context.Notifications.AddAsync(new Notification
//                    {
//                        UserID = driver.Id,
//                        Message = "Your driver application is pending approval by the admin.",
//                        Timestamp = DateTime.UtcNow,
//                        Status = "Unread"
//                    }, cancellationToken);
//                }
//            }
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched {DriverCount} pending drivers", GetCurrentAdminId(), pendingDrivers.Count);
//            return Success(pendingDrivers, $"Successfully fetched {pendingDrivers.Count} pending drivers");
//        }

//        public async Task<Response<DriverResponse>> Handle(GetDriverDetailsQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching details for driver {DriverId}...", GetCurrentAdminId(), request.DriverId);

//            var driver = await _userManager.FindByIdAsync(request.DriverId);
//            if (driver == null || !await _userManager.IsInRoleAsync(driver, "Driver"))
//            {
//                _logger.Warning("Driver {DriverId} not found for admin {AdminId}", request.DriverId, GetCurrentAdminId());
//                return NotFound<DriverResponse>("Driver not found");
//            }

//            var response = new DriverResponse
//            {
//                Id = driver.Id,
//                Name = driver.UserName,
//                Email = driver.Email,
//                PhoneNumber = driver.PhoneNumber,
//                CompanyId = driver.CompanyID,
//                IsApproved = driver.IsApproved,
//                VehiclesDriven = driver.VehiclesDriven?.Select(v => new VehicleResponse
//                {
//                    VehicleId = v.VehicleID,
//                    Model = v.Model,
//                    Capacity = v.Capacity
//                }).ToList()
//            };

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetDriverDetails",
//                Description = $"Admin {GetCurrentAdminId()} fetched details for driver {driver.Id}",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched details for driver {DriverId}", GetCurrentAdminId(), driver.Id);
//            return Success(response, "Driver details retrieved successfully");
//        }

//        public async Task<Response<List<TripResponse>>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching trips with status {Status}, start date {StartDate}, end date {EndDate}...",
//                GetCurrentAdminId(), request.Status ?? "Any", request.StartDate?.ToString() ?? "N/A", request.EndDate?.ToString() ?? "N/A");

//            var tripsQuery = _context.Trips.AsQueryable();
//            if (!string.IsNullOrEmpty(request.Status))
//                tripsQuery = tripsQuery.Where(t => t.Status == request.Status);
//            if (request.StartDate.HasValue)
//                tripsQuery = tripsQuery.Where(t => t.StartTime >= request.StartDate.Value);
//            if (request.EndDate.HasValue)
//                tripsQuery = tripsQuery.Where(t => t.StartTime <= request.EndDate.Value);

//            var trips = await tripsQuery
//                .Select(t => new TripResponse
//                {
//                    TripId = t.TripID,
//                    RiderId = t.RiderTrips.FirstOrDefault()?.RiderId,
//                    DriverId = t.DriverID,
//                    StartLocation = t.StartLocation,
//                    EndLocation = t.EndLocation,
//                    StartTime = t.StartTime,
//                    EndTime = t.EndTime,
//                    Status = t.Status,
//                    Fare = t.FarePerRider
//                })
//                .ToListAsync(cancellationToken);

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetTrips",
//                Description = $"Admin {GetCurrentAdminId()} fetched {trips.Count} trips with status {request.Status ?? "Any"}",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched {TripCount} trips", GetCurrentAdminId(), trips.Count);
//            return Success(trips, $"Successfully fetched {trips.Count} trips");
//        }

//        public async Task<Response<TripResponse>> Handle(GetTripDetailsQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching details for trip {TripId}...", GetCurrentAdminId(), request.TripId);

//            var trip = await _context.Trips
//                .Where(t => t.TripID == request.TripId)
//                .Select(t => new TripResponse
//                {
//                    TripId = t.TripID,
//                    RiderId = t.RiderTrips.FirstOrDefault()?.RiderId,
//                    DriverId = t.DriverID,
//                    StartLocation = t.StartLocation,
//                    EndLocation = t.EndLocation,
//                    StartTime = t.StartTime,
//                    EndTime = t.EndTime,
//                    Status = t.Status,
//                    Fare = t.FarePerRider
//                })
//                .FirstOrDefaultAsync(cancellationToken);

//            if (trip == null)
//            {
//                _logger.Warning("Trip {TripId} not found for admin {AdminId}", request.TripId, GetCurrentAdminId());
//                return NotFound<TripResponse>("Trip not found");
//            }

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetTripDetails",
//                Description = $"Admin {GetCurrentAdminId()} fetched details for trip {trip.TripId}",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            // Notify the driver if the trip is ongoing
//            if (trip.Status == "Ongoing")
//            {
//                await _context.Notifications.AddAsync(new Notification
//                {
//                    UserID = trip.DriverId,
//                    Message = $"Admin has reviewed your ongoing trip {trip.TripId}. Please ensure everything is in order.",
//                    Timestamp = DateTime.UtcNow,
//                    Status = "Unread"
//                }, cancellationToken);
//                await _context.SaveChangesAsync(cancellationToken);
//            }

//            _logger.Information("Admin {AdminId} successfully fetched details for trip {TripId}", GetCurrentAdminId(), trip.TripId);
//            return Success(trip, "Trip details retrieved successfully");
//        }

//        public async Task<Response<UsageAnalyticsResponse>> Handle(GetUsageAnalyticsQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching usage analytics from {StartDate} to {EndDate}...",
//                GetCurrentAdminId(), request.StartDate?.ToString() ?? "N/A", request.EndDate?.ToString() ?? "N/A");

//            var tripsQuery = _context.Trips.AsQueryable();
//            if (request.StartDate.HasValue)
//                tripsQuery = tripsQuery.Where(t => t.StartTime >= request.StartDate.Value);
//            if (request.EndDate.HasValue)
//                tripsQuery = tripsQuery.Where(t => t.StartTime <= request.EndDate.Value);

//            var response = new UsageAnalyticsResponse
//            {
//                TotalRides = await tripsQuery.CountAsync(cancellationToken),
//                TotalRevenue = await tripsQuery.Where(t => t.Status == "Completed").SumAsync(t => t.FarePerRider, cancellationToken),
//                UserGrowth = await _context.Users
//                    .GroupBy(u => u.LockoutEnd.HasValue ? u.LockoutEnd.Value.Date : DateTime.UtcNow.Date)
//                    .Select(g => new UserGrowthData { Date = g.Key, UserCount = g.Count() })
//                    .ToListAsync(cancellationToken)
//            };

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetUsageAnalytics",
//                Description = $"Admin {GetCurrentAdminId()} fetched usage analytics",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched usage analytics", GetCurrentAdminId());
//            return Success(response, "Usage analytics retrieved successfully");
//        }

//        public async Task<Response<DriverAnalyticsResponse>> Handle(GetDriverAnalyticsQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching driver analytics...", GetCurrentAdminId());

//            var drivers = await _userManager.GetUsersInRoleAsync("Driver");
//            var response = new DriverAnalyticsResponse
//            {
//                TopDriversByEarnings = await _context.Trips
//                    .Where(t => t.Status == "Completed")
//                    .GroupBy(t => t.DriverID)
//                    .OrderByDescending(g => g.Sum(t => t.FarePerRider))
//                    .Take(5)
//                    .Select(g => new DriverPerformance
//                    {
//                        DriverId = g.Key,
//                        Name = drivers.First(d => d.Id == g.Key).UserName,
//                        Earnings = g.Sum(t => t.FarePerRider),
//                        AverageRating = drivers.First(d => d.Id == g.Key).Rating
//                    })
//                    .ToListAsync(cancellationToken),
//                TopDriversByRatings = await _context.Reviews
//                    .GroupBy(r => r.RevieweeID)
//                    .OrderByDescending(g => g.Average(r => r.Rating))
//                    .Take(5)
//                    .Select(g => new DriverPerformance
//                    {
//                        DriverId = g.Key,
//                        Name = drivers.First(d => d.Id == g.Key).UserName,
//                        Earnings = _context.Trips.Where(t => t.DriverID == g.Key && t.Status == "Completed").Sum(t => t.FarePerRider),
//                        AverageRating = (float)g.Average(r => r.Rating)
//                    })
//                    .ToListAsync(cancellationToken),
//                UnderperformingDrivers = await _context.Trips
//                    .GroupBy(t => t.DriverID)
//                    .OrderBy(g => g.Count())
//                    .Take(5)
//                    .Select(g => new DriverPerformance
//                    {
//                        DriverId = g.Key,
//                        Name = drivers.First(d => d.Id == g.Key).UserName,
//                        Earnings = g.Where(t => t.Status == "Completed").Sum(t => t.FarePerRider),
//                        AverageRating = drivers.First(d => d.Id == g.Key).Rating
//                    })
//                    .ToListAsync(cancellationToken)
//            };

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetDriverAnalytics",
//                Description = $"Admin {GetCurrentAdminId()} fetched driver analytics",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            // Notify underperforming drivers
//            foreach (var driver in response.UnderperformingDrivers)
//            {
//                await _context.Notifications.AddAsync(new Notification
//                {
//                    UserID = driver.DriverId,
//                    Message = "Your performance has been flagged as underperforming by the admin. Please improve your activity.",
//                    Timestamp = DateTime.UtcNow,
//                    Status = "Unread"
//                }, cancellationToken);
//            }
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched driver analytics", GetCurrentAdminId());
//            return Success(response, "Driver analytics retrieved successfully");
//        }

//        public async Task<Response<VehicleAnalyticsResponse>> Handle(GetVehicleAnalyticsQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is fetching vehicle analytics...", GetCurrentAdminId());

//            var response = new VehicleAnalyticsResponse
//            {
//                MostUsedVehicles = await _context.TripRequests
//                    .Where(tr => tr.VehicleID != null)
//                    .GroupBy(tr => tr.VehicleID)
//                    .OrderByDescending(g => g.Count())
//                    .Take(5)
//                    .Select(g => new VehicleUsage
//                    {
//                        VehicleId = g.Key,
//                        Model = _context.Vehicles.First(v => v.VehicleID == g.Key).Model,
//                        TripCount = g.Count()
//                    })
//                    .ToListAsync(cancellationToken),
//                UnderutilizedVehicles = await _context.TripRequests
//                    .Where(tr => tr.VehicleID != null)
//                    .GroupBy(tr => tr.VehicleID)
//                    .OrderBy(g => g.Count())
//                    .Take(5)
//                    .Select(g => new VehicleUsage
//                    {
//                        VehicleId = g.Key,
//                        Model = _context.Vehicles.First(v => v.VehicleID == g.Key).Model,
//                        TripCount = g.Count()
//                    })
//                    .ToListAsync(cancellationToken)
//            };

//            // Log the action in AuditLog
//            await _context.AuditLogs.AddAsync(new AuditLog
//            {
//                AdminId = GetCurrentAdminId(),
//                Action = "GetVehicleAnalytics",
//                Description = $"Admin {GetCurrentAdminId()} fetched vehicle analytics",
//                Timestamp = DateTime.UtcNow
//            }, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            _logger.Information("Admin {AdminId} successfully fetched vehicle analytics", GetCurrentAdminId());
//            return Success(response, "Vehicle analytics retrieved successfully");
//        }

//        public async Task<Response<string>> Handle(GenerateReportQuery request, CancellationToken cancellationToken)
//        {
//            _logger.Information("Admin {AdminId} is generating a report with format {Format}, region {Region}, user type {UserType}, date {Date}...",
//                GetCurrentAdminId(), request.Format, request.Region ?? "N/A", request.UserType ?? "N/A", request.Date?.ToString() ?? "N/A");

//            var tripsQuery = _context.Trips.AsQueryable();
//            if (!string.IsNullOrEmpty(request.Region))
//                tripsQuery = tripsQuery.Where(t => t.StartLocation.Contains(request.Region) || t.EndLocation.Contains(request.Region));
//            if (!string.IsNullOrEmpty(request.UserType))
//                tripsQuery = tripsQuery.Where(t => t.RiderTrips.Any(rt => rt.Rider.Role == request.UserType));
//            if (request.Date.HasValue)
//                tripsQuery = tripsQuery.Where(t => t.StartTime.Date == request.Date.Value.Date);

//            var trips = await tripsQuery
//                .Select(t => new
//                {
//                    t.TripID,
//                    RiderId = t.RiderTrips.FirstOrDefault()?.RiderId,
//                    t.DriverID,
//                    t.StartLocation,
//                    t.EndLocation,
//                    t.Status,
//                    t.FarePerRider
//                })
//                .ToListAsync(cancellationToken);

//            if (request.Format.ToUpper() == "PDF")
//            {
//                using (var memoryStream = new MemoryStream())
//                {
//                    var document = new Document(PageSize.A4, 10, 10, 10, 10);
//                    PdfWriter.GetInstance(document, memoryStream);
//                    document.Open();

//                    document.Add(new Paragraph("TripPool Admin Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18)));
//                    document.Add(new Paragraph($"Generated on: {DateTime.UtcNow}", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
//                    document.Add(new Paragraph("\n"));

//                    PdfPTable table = new PdfPTable(7);
//                    table.WidthPercentage = 100;
//                    table.AddCell(new PdfPCell(new Phrase("TripId", FontFactory.GetFont(FontFactory.HELVETICA_BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
//                    table.AddCell(new PdfPCell(new Phrase("RiderId", FontFactory.GetFont(FontFactory.HELVETICA_BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
//                    table.AddCell(new PdfPCell(new Phrase("DriverId", FontFactory.GetFont(FontFactory.HELVETICA_BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
//                    table.AddCell(new PdfPCell(new Phrase("StartLocation", FontFactory.GetFont(FontFactory.HELVETICA_BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
//                    table.AddCell(new PdfPCell(new Phrase("EndLocation", FontFactory.GetFont(FontFactory.HELVETICA_BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
//                    table.AddCell(new PdfPCell(new Phrase("Status", FontFactory.GetFont(FontFactory.HELVETICA_BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
//                    table.AddCell(new PdfPCell(new Phrase("Fare", FontFactory.GetFont(FontFactory.HELVETICA_BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });

//                    foreach (var trip in trips)
//                    {
//                        table.AddCell(trip.TripID.ToString());
//                        table.AddCell(trip.RiderId ?? "N/A");
//                        table.AddCell(trip.DriverID);
//                        table.AddCell(trip.StartLocation);
//                        table.AddCell(trip.EndLocation);
//                        table.AddCell(trip.Status);
//                        table.AddCell(trip.FarePerRider.ToString("F2"));
//                    }

//                    document.Add(table);
//                    document.Close();

//                    var pdfBytes = memoryStream.ToArray();
//                    var base64String = Convert.ToBase64String(pdfBytes);

//                    // Log the action in AuditLog
//                    await _context.AuditLogs.AddAsync(new AuditLog
//                    {
//                        AdminId = GetCurrentAdminId(),
//                        Action = "GenerateReport",
//                        Description = $"Admin {GetCurrentAdminId()} generated a report with format {request.Format}",
//                        Timestamp = DateTime.UtcNow
//                    }, cancellationToken);
//                    await _context.SaveChangesAsync(cancellationToken);

//                    _logger.Information("Admin {AdminId} successfully generated a report", GetCurrentAdminId());
//                    return Success(base64String, "Report generated successfully");
//                }
//            }
//            else
//            {
//                _logger.Warning("Admin {AdminId} attempted to generate a report with unsupported format {Format}", GetCurrentAdminId(), request.Format);
//                return BadRequest<string>("Only PDF format is supported for now");
//            }
//        }

//        private string GetCurrentAdminId()
//        {
//            return "9E466E21-3B55-4F4D-8D99-123456789999"; // Hardcoded admin ID
//        }
//    }
//}