using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly UserManager<User> _userManager;

        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _context;

        public NotificationHub(
            IMemoryCache memoryCache,
            UserManager<User> userManager,

            ApplicationDbContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
            _userManager = userManager;

        }





    }
}

