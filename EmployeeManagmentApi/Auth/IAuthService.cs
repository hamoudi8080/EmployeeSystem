using Shared.Models;

namespace EmployeeManagmentApi.Auth
{
    public interface IAuthService
    {
        Task<User> GetUser(string username, string password);
        Task<User> ValidateUser(string username, string password);
        Task RegisterUser(User user);
    }
}
