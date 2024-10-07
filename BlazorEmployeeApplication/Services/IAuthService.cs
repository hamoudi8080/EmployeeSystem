using EmployeeManagmentModel;
using System.Security.Claims;

namespace BlazorEmployeeApplication.Services
{
    public interface IAuthService
    {
        public Task<string> LoginAsync(string username, string password);
        public Task<Task> LogoutAsync();
        public Task RegisterAsync(Admin user);
        public Task<ClaimsPrincipal> GetAuthAsync();
    
        public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    }
}
