using EmployeeManagmentApi.Models;
using EmployeeManagmentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagmentApi.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext appDbContext;

        public AuthService(AppDbContext appDbContext) {
            this.appDbContext = appDbContext;
        }

        private readonly IList<Admin> users = new List<Admin>
    {
        new Admin
        {
            Age = 36,
            Email = "trmo@via.dk",
            Domain = "via",
            Name = "hamo",
            Password = "1234",
            Role = "Teacher",
            Username = "hamo",
            SecurityLevel = 4
        },
        new Admin
        {
            Age = 34,
            Email = "jakob@gmail.com",
            Domain = "gmail",
            Name = "Jakob Rasmussen",
            Password = "12345",
            Role = "Student",
            Username = "jk1nr",
            SecurityLevel = 2
        }
    };

        public Task<Admin> ValidateUser(string username, string password)
        {
            Admin? existingUser = users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            if (!existingUser.Password.Equals(password))
            {
                throw new Exception("Password mismatch");
            }

            return Task.FromResult(existingUser);
        }

        public async Task<Admin> GetUser(string username, string password)
        {
            Admin? existingUser = await appDbContext.User
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

            if (existingUser == null)
            {
                throw new ValidationException("User not found");
            }

            if (!existingUser.Password.Equals(password))
            {
                throw new ValidationException("Password mismatch");
            }

            return existingUser;
        }

        public async Task<Admin> RegisterAdmin(Admin user)
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ValidationException("Username cannot be null");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ValidationException("Password cannot be null");
            }

            //users.Add(user);

            // Save changes to the database
            await appDbContext.SaveChangesAsync();

            // Return the registered user
            return user;
        }
    }
}
