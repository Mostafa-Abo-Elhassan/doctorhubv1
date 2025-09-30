using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs
{
    public class TrackingHub : Hub
    {
        public async Task SendLocationUpdate(string rideId, string driverLocation, string eta, string route)
        {
            await Clients.Group(rideId).SendAsync("ReceiveLocationUpdate", driverLocation, eta, route);
        }

        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

        public async Task JoinRideGroup(string rideId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, rideId);
        }

        public async Task LeaveRideGroup(string rideId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, rideId);
        }

        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        public async Task LeaveUserGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; // يتم استخراج UserId من الـ JWT
            if (!string.IsNullOrEmpty(userId))
            {
                await JoinUserGroup(userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await LeaveUserGroup(userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}