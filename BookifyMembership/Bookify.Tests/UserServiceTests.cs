using Xunit;
using Microsoft.AspNetCore.Identity;
using BookifyMembership.Services;
using BookifyMembership.Models.Dtos;

namespace Bookify.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService();
        }

        [Fact]
        public void Register_User_Success()
        {
            var registerRequest = new RegisterRequest
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Test@1234"
            };

           
            var result = _userService.Register(registerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
            Assert.Equal("test@example.com", result.Email);
            Assert.NotEqual("Test@1234", result.PasswordHash);
        }

        [Fact]
        public void Register_User_Fail_ExistingEmail()
        {
            var existingUser = new RegisterRequest
            {
                Username = "existinguser",
                Email = "test@example.com",
                Password = "Password123"
            };

            _userService.Register(existingUser);
            var secondRegisterRequest = new RegisterRequest
            {
                Username = "seconduser",
                Email = "test@example.com", 
                Password = "NewPassword123"
            };
            var result = _userService.Register(secondRegisterRequest);

            Assert.Null(result);
        }
    }
}
