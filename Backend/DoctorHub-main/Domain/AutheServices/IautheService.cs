using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;
namespace Domain.AutheServices
{
    public interface IautheService
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> CreateUserByObjectAsync(User user);

        Task AddToRoleAsync(User user, String role);
        Task<IdentityResult> UpdateAsync(User user);

        Task<User> GetUserByIdAsync(string id);
        Task<User> FindUserByEmailAsync(string email);
        Task<User?> FindUserByNationalIdAsync(string nationalId);
        
            //Task<User> FindUserByIdAsync(string email);

            Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool rememberme);
        Task<IdentityResult> ResetPasswordAsync(User user, string NewPassword);
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<ICollection<string>> GetRolesAsync(User user);
        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<User> FindUserByLoginAsync(string loginProvider, string providerKey);
        Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo login);


    }
}
