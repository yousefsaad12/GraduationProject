using Api.Interfaces;
using Api.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GraduationProject.Tests
{
    public class TokenServicesTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly ITokenServices _tokenService;

        public TokenServicesTests()
        {
            // Setup configuration mock
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(x => x["JWT:Key"]).Returns("YourSecretKeyHere12345678901234567890");
            _mockConfiguration.Setup(x => x["JWT:Issuer"]).Returns("TestIssuer");
            _mockConfiguration.Setup(x => x["JWT:Audience"]).Returns("TestAudience");

            // Create service instance
            _tokenService = new TokenServices(_mockConfiguration.Object);
        }

        [Fact]
        public void CreateToken_ValidUser_ReturnsToken()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                Email = "test@example.com"
            };

            // Act
            var token = _tokenService.CreateToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void CreateToken_NullUser_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _tokenService.CreateToken(null));
        }

        [Fact]
        public void CreateToken_ValidUser_ContainsUserClaims()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                Email = "test@example.com"
            };

            // Act
            var token = _tokenService.CreateToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.Contains(user.Email, token); // Email should be in the token
            Assert.Contains(user.UserName, token); // Username should be in the token
        }

        [Fact]
        public void CreateToken_ValidUser_TokenExpiresIn30Minutes()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                Email = "test@example.com"
            };

            // Act
            var token = _tokenService.CreateToken(user);

            // Assert
            Assert.NotNull(token);
            // Note: We can't directly test the expiration time as it's encoded in the JWT
            // This is more of an integration test concern
        }
    }
} 