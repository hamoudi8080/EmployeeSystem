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
  
        }
    };

        public async Task<Admin> ValidateUser(string username, string password)
        {

            Admin? existingUser = await appDbContext.Admin
       .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, existingUser.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Password mismatch");
            }

            return existingUser;
        }



        public async Task<Admin> GetUser(string username, string password)
        {
            Admin? existingUser = await appDbContext.Admin
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

            // Hash the password using BCrypt (which includes salting)
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            // Add the user to the database
            await appDbContext.Admin.AddAsync(user);

            // Save changes to the database
            await appDbContext.SaveChangesAsync();

          

            // Return the registered user
            return user;
        }
    }
}
