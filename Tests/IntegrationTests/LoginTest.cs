using EmployeeManagmentApi.Auth;
using EmployeeManagmentApi.Models;
using EmployeeManagmentModel;
using Microsoft.EntityFrameworkCore;
 
 
 
namespace Tests.UnitTests
{
    public class LoginTest
    {
        public static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("TestDatabase");
            return builder.Options;
        }

        [Fact]
        public async Task ValidateUser_ShouldReturnUser_WhenCredentialsAreValid()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password");
            await context.Admin.AddAsync(new Admin
            {
                Username = "test",
                Password = hashedPassword,
                Domain = "example.com",
                Email = "test@test.com",
                Name = "Test User",
                Role = "Admin"
            });
            await context.SaveChangesAsync();
            var service = new AuthService(context);

            // Act
            var result = await service.ValidateUser("test", "password");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test", result.Username);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task ValidateUser_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            var service = new AuthService(context);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.ValidateUser("nonexistentuser@test.com", "password"));
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task ValidateUser_ShouldThrowException_WhenPasswordMismatch()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password");
            await context.Admin.AddAsync(new Admin
            {
                Username = "test",
                Password = hashedPassword,
                Domain = "example.com",
                Email = "test@test.com",
                Name = "Test User",
                Role = "Admin"
            });
            await context.SaveChangesAsync();
            var service = new AuthService(context);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.ValidateUser("test@test.com", "wrongpassword"));
            await context.Database.EnsureDeletedAsync();
        }
    }
}
