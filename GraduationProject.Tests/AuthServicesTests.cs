using Api.Dto;
using Api.Interfaces;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace GraduationProject.Tests
{
    public class AuthServicesTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly IAuthServices _authService;

        public AuthServicesTests()
        {
            // Setup UserManager mock
            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object,
                null, null, null, null, null, null, null, null);

            // Setup SignInManager mock
            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            // Create service instance
            _authService = new AuthServices(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsSuccess()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                userName = "testuser",
                email = "test@example.com",
                password = "Test123!"
            };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.Register(userRequest);

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task Register_InvalidUser_ReturnsFailure()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                userName = "testuser",
                email = "test@example.com",
                password = "weak"
            };

            var errors = new[] { new IdentityError { Description = "Password is too weak" } };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors));

            // Act
            var result = await _authService.Register(userRequest);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains(errors[0].Description, result.Errors.Select(e => e.Description));
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var loginReq = new LoginReq
            {
                email = "test@example.com",
                passWord = "Test123!"
            };

            var user = new User { UserName = "testuser", Email = "test@example.com" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(loginReq.email))
                .ReturnsAsync(user);

            _mockSignInManager.Setup(x => x.CheckPasswordSignInAsync(user, loginReq.passWord, false))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _authService.Login(loginReq, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var loginReq = new LoginReq
            {
                email = "test@example.com",
                passWord = "WrongPassword"
            };

            var user = new User { UserName = "testuser", Email = "test@example.com" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(loginReq.email))
                .ReturnsAsync(user);

            _mockSignInManager.Setup(x => x.CheckPasswordSignInAsync(user, loginReq.passWord, false))
                .ReturnsAsync(SignInResult.Failed);

            // Act
            var result = await _authService.Login(loginReq, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUser_ExistingUser_ReturnsSuccess()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { UserName = "testuser", Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);

            _mockUserManager.Setup(x => x.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.DeleteUser(email);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(email, result.Data);
        }

        [Fact]
        public async Task DeleteUser_NonExistingUser_ReturnsFailure()
        {
            // Arrange
            var email = "nonexistent@example.com";
            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authService.DeleteUser(email);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("UserNotFound", result.Error);
        }
    }
} 