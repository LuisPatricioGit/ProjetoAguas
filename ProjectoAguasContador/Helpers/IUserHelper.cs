using Microsoft.AspNetCore.Identity;
using ProjectoAguasContador.Data.Entities;
using ProjectoAguasContador.Models;
using System.Threading.Tasks;

namespace ProjectoAguasContador.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByIdAsync(string id);

        Task<IdentityResult> AddUserNoPassAsync(User user);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<IdentityResult> DeleteUserAsync(User user);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        bool IsUserInRole(User user, string roleName);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);


    }
}
