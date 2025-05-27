using GraduationProject.InterFaces;
using GraduationProject.Services;
using GraduationProject.Data;
using GraduationProject.Dtos;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace GraduationProject.Tests
{
    public class DoctorServicesTests
    {
        private readonly Mock<ApplicationDb> _mockContext;
        private readonly IDoctorInterface _doctorService;

        public DoctorServicesTests()
        {
            // Setup mock DbContext
            var options = new DbContextOptionsBuilder<ApplicationDb>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _mockContext = new Mock<ApplicationDb>(options);
            
            // Create service instance
            _doctorService = new DoctorServices(_mockContext.Object);
        }

        [Fact]
        public async Task AddDoctor_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new CreateDoctorRequest
            {
                name = "Dr. John Doe",
                specialization = "Cardiology",
                email = "john.doe@example.com",
                phoneNumber = "1234567890",
                country = "USA",
                location = "New York"
            };

            // Act
            var result = await _doctorService.AddDoctor(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Doctor added successfully.", result.Message);
        }

        [Fact]
        public async Task AddDoctor_NullRequest_ReturnsFailure()
        {
            // Act
            var result = await _doctorService.AddDoctor(null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Request is null.", result.Error);
        }

        [Fact]
        public async Task AddDoctor_DuplicateEmail_ReturnsFailure()
        {
            // Arrange
            var request = new CreateDoctorRequest
            {
                name = "Dr. John Doe",
                specialization = "Cardiology",
                email = "existing@example.com",
                phoneNumber = "1234567890"
            };

            // Setup mock to return true for IsEmailExist
            _mockContext.Setup(x => x.Doctors.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Doctor, bool>>>(), default))
                .ReturnsAsync(true);

            // Act
            var result = await _doctorService.AddDoctor(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Email already exists.", result.Error);
        }

        [Fact]
        public async Task GetAllDoctors_ReturnsListOfDoctors()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
                new Doctor { Id = 1, Name = "Dr. John", Email = "john@example.com" },
                new Doctor { Id = 2, Name = "Dr. Jane", Email = "jane@example.com" }
            };

            var mockDbSet = doctors.AsQueryable().BuildMockDbSet();
            _mockContext.Setup(x => x.Doctors).Returns(mockDbSet.Object);

            // Act
            var result = await _doctorService.GetAllDoctors();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DeleteDoctor_ExistingId_ReturnsSuccess()
        {
            // Arrange
            var doctor = new Doctor { Id = 1, Name = "Dr. John", Email = "john@example.com" };
            var mockDbSet = new List<Doctor> { doctor }.AsQueryable().BuildMockDbSet();
            
            _mockContext.Setup(x => x.Doctors).Returns(mockDbSet.Object);
            _mockContext.Setup(x => x.Doctors.FindAsync(1)).ReturnsAsync(doctor);

            // Act
            var result = await _doctorService.DeleteDoctor(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Doctor has been deleted successfully.", result.Message);
        }

        [Fact]
        public async Task DeleteDoctor_NonExistingId_ReturnsFailure()
        {
            // Arrange
            var mockDbSet = new List<Doctor>().AsQueryable().BuildMockDbSet();
            _mockContext.Setup(x => x.Doctors).Returns(mockDbSet.Object);

            // Act
            var result = await _doctorService.DeleteDoctor(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Doctor with the provided ID does not exist.", result.Error);
        }
    }

    // Helper extension method for mocking DbSet
    public static class MockDbSetExtensions
    {
        public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }
    }
} 