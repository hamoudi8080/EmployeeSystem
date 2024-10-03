using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagmentApi.Auth
{
    public class AuthService : IAuthService
    {

        private readonly IList<User> users = new List<User>
    {
        new User
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
        new User
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

        public Task<User> ValidateUser(string username, string password)
        {
            User? existingUser = users.FirstOrDefault(u =>
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

        public Task<User> GetUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(User user)
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ValidationException("Username cannot be null");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ValidationException("Password cannot be null");
            }
            // Do more user info validation here

            // save to persistence instead of list

            users.Add(user);

            return Task.CompletedTask;
        }
    }
}
