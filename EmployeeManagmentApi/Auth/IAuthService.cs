using EmployeeManagmentModel;

namespace EmployeeManagmentApi.Auth
{
    public interface IAuthService
    {
        Task<Admin> GetUser(string username, string password);
        Task<Admin> ValidateUser(string username, string password);
        Task<Admin> RegisterAdmin(Admin admin);
    }
}
