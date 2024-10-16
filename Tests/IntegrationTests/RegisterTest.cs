using EmployeeManagmentApi.Auth;
using EmployeeManagmentApi.Controllers;
using EmployeeManagmentApi.Models;
using EmployeeManagmentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

 
 
 

namespace Tests.IntegrationTests
{
    public class RegisterTest
    {
        public static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("TestDatabase");
            return builder.Options;
        }

        [Fact]
        public async Task RegisterAdmin_ShouldReturnOk_WhenAdminIsRegisteredSuccessfully()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();

            var admin = new Admin
            {
                Username = "test2",
                Password = "123",
                Domain = "example.com",
                Email = "test@test.com",
                Name = "Test User",
                Role = "Admin"
            };
            var service = new AuthService(context);

            // Act
            var result = await service.RegisterAdmin(admin);

            // Assert
            var savedAdmin = await context.Admin.FirstOrDefaultAsync(a => a.Username == "test2");
            Assert.NotNull(savedAdmin);
            Assert.Equal("test2", result.Username);
            Assert.Equal(admin.Email, savedAdmin.Email);
            Assert.Equal(admin.Password, savedAdmin.Password);
            await context.Database.EnsureDeletedAsync();
        }


        [Fact]
        public async Task RegisterAdmin_ShouldThrowException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();

            var existingAdmin = new Admin
            {
                Username = "duplicateUser",
                Password = "password123",
                Domain = "example.com",
                Email = "duplicate@test.com",
                Name = "Duplicate User",
                Role = "Admin"
            };

            await context.Admin.AddAsync(existingAdmin);
            await context.SaveChangesAsync();

            var newAdmin = new Admin
            {
                Username = "duplicateUser", // Same username as existing admin
                Password = "newpassword",
                Domain = "example.com",
                Email = "new@test.com",
                Name = "New User",
                Role = "Admin"
            };

            var service = new AuthService(context);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAdmin(newAdmin));

            await context.Database.EnsureDeletedAsync();
        }


    }
}
