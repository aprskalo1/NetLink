using Microsoft.AspNetCore.Mvc;
using Moq;
using NetLink.API.Controllers;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Services;

namespace NetLink.Tests.Controllers;

public class DevelopersControllerTests
{
    private readonly Mock<IDeveloperService> _developerServiceMock;
    private readonly DevelopersController _controller;

    public DevelopersControllerTests()
    {
        _developerServiceMock = new Mock<IDeveloperService>();
        _controller = new DevelopersController(_developerServiceMock.Object);
    }

    [Fact]
    public async Task AddDeveloperAsync_ValidModel_ReturnsOkResultWithGuid()
    {
        // Arrange
        var developerRequestDto = new DeveloperRequestDto
        {
            Username = "testuser"
        };

        var expectedDeveloperId = Guid.NewGuid();
        _developerServiceMock
            .Setup(s => s.AddDeveloperAsync(developerRequestDto))
            .ReturnsAsync(expectedDeveloperId);

        // Act
        var result = await _controller.AddDeveloperAsync(developerRequestDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedDeveloperId, okResult.Value);
        _developerServiceMock.Verify(s => s.AddDeveloperAsync(developerRequestDto), Times.Once);
    }

    [Fact]
    public async Task AddDeveloperAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Username", "Required");

        var developerRequestDto = new DeveloperRequestDto
        {
            // Leave out required properties to make the model invalid
        };

        // Act
        var result = await _controller.AddDeveloperAsync(developerRequestDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
        _developerServiceMock.Verify(s => s.AddDeveloperAsync(It.IsAny<DeveloperRequestDto>()), Times.Never);
    }

    [Fact]
    public async Task GetDeveloperFromTokenAsync_ValidToken_ReturnsOkResultWithDeveloperId()
    {
        // Arrange
        const string token = "validToken";
        var expectedDeveloperId = Guid.NewGuid();
        _developerServiceMock
            .Setup(s => s.GetDeveloperIdFromTokenAsync(token))
            .ReturnsAsync(expectedDeveloperId);

        // Act
        var result = await _controller.GetDeveloperFromTokenAsync(token);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedDeveloperId, okResult.Value);
        _developerServiceMock.Verify(s => s.GetDeveloperIdFromTokenAsync(token), Times.Once);
    }

    [Fact]
    public async Task GetDeveloperByIdAsync_ExistingId_ReturnsOkResultWithDeveloper()
    {
        // Arrange
        var developerId = Guid.NewGuid();
        var developerResponseDto = new DeveloperResponseDto
        {
            Id = developerId,
            Username = "testuser"
        };

        _developerServiceMock
            .Setup(s => s.GetDeveloperByIdAsync(developerId))
            .ReturnsAsync(developerResponseDto);

        // Act
        var result = await _controller.GetDeveloperByIdAsync(developerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(developerResponseDto, okResult.Value);
        _developerServiceMock.Verify(s => s.GetDeveloperByIdAsync(developerId), Times.Once);
    }

    [Fact]
    public async Task GetDeveloperByUsernameAsync_ExistingUsername_ReturnsOkResultWithDeveloper()
    {
        // Arrange
        const string username = "testuser";
        var developerResponseDto = new DeveloperResponseDto
        {
            Id = Guid.NewGuid(),
            Username = username
        };

        _developerServiceMock
            .Setup(s => s.GetDeveloperByUsernameAsync(username))
            .ReturnsAsync(developerResponseDto);

        // Act
        var result = await _controller.GetDeveloperByUsernameAsync(username);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(developerResponseDto, okResult.Value);
        _developerServiceMock.Verify(s => s.GetDeveloperByUsernameAsync(username), Times.Once);
    }

    [Fact]
    public async Task UpdateDeveloperAsync_ValidModel_ReturnsOkResultWithUpdatedDeveloper()
    {
        // Arrange
        var developerId = Guid.NewGuid();
        var developerRequestDto = new DeveloperRequestDto
        {
            Username = "updateduser"
        };

        var developerResponseDto = new DeveloperResponseDto
        {
            Id = developerId,
            Username = developerRequestDto.Username
            // Populate other necessary properties
        };

        _developerServiceMock
            .Setup(s => s.UpdateDeveloperAsync(developerId, developerRequestDto))
            .ReturnsAsync(developerResponseDto);

        // Act
        var result = await _controller.UpdateDeveloperAsync(developerId, developerRequestDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(developerResponseDto, okResult.Value);
        _developerServiceMock.Verify(s => s.UpdateDeveloperAsync(developerId, developerRequestDto), Times.Once);
    }

    [Fact]
    public async Task UpdateDeveloperAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Email", "Invalid email format");

        var developerId = Guid.NewGuid();
        var developerRequestDto = new DeveloperRequestDto
        {
            // Provide invalid email or missing required fields
        };

        // Act
        var result = await _controller.UpdateDeveloperAsync(developerId, developerRequestDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        _developerServiceMock.Verify(s => s.UpdateDeveloperAsync(It.IsAny<Guid>(), It.IsAny<DeveloperRequestDto>()), Times.Never);
    }

    [Fact]
    public async Task DeactivateDeveloperAsync_ValidId_ReturnsOk()
    {
        // Arrange
        var developerId = Guid.NewGuid();
        _developerServiceMock.Setup(s => s.DeactivateDeveloperAsync(developerId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeactivateDeveloperAsync(developerId);

        // Assert
        Assert.IsType<OkResult>(result);
        _developerServiceMock.Verify(s => s.DeactivateDeveloperAsync(developerId), Times.Once);
    }

    [Fact]
    public async Task ReactivateDeveloperAsync_ValidId_ReturnsOk()
    {
        // Arrange
        var developerId = Guid.NewGuid();
        _developerServiceMock.Setup(s => s.ReactivateDeveloperAsync(developerId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.ReactivateDeveloperAsync(developerId);

        // Assert
        Assert.IsType<OkResult>(result);
        _developerServiceMock.Verify(s => s.ReactivateDeveloperAsync(developerId), Times.Once);
    }

    [Fact]
    public async Task SoftDeleteDeveloperAsync_ValidId_ReturnsOk()
    {
        // Arrange
        var developerId = Guid.NewGuid();
        _developerServiceMock.Setup(s => s.SoftDeleteDeveloperAsync(developerId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.SoftDeleteDeveloperAsync(developerId);

        // Assert
        Assert.IsType<OkResult>(result);
        _developerServiceMock.Verify(s => s.SoftDeleteDeveloperAsync(developerId), Times.Once);
    }

    [Fact]
    public async Task RestoreDeveloperAsync_ValidId_ReturnsOk()
    {
        // Arrange
        var developerId = Guid.NewGuid();
        _developerServiceMock.Setup(s => s.RestoreDeveloperAsync(developerId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RestoreDeveloperAsync(developerId);

        // Assert
        Assert.IsType<OkResult>(result);
        _developerServiceMock.Verify(s => s.RestoreDeveloperAsync(developerId), Times.Once);
    }

    [Fact]
    public async Task DeleteDeveloperAsync_ValidId_ReturnsOk()
    {
        // Arrange
        var developerId = Guid.NewGuid();
        _developerServiceMock.Setup(s => s.DeleteDeveloperAsync(developerId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteDeveloperAsync(developerId);

        // Assert
        Assert.IsType<OkResult>(result);
        _developerServiceMock.Verify(s => s.DeleteDeveloperAsync(developerId), Times.Once);
    }

    [Fact]
    public async Task ListDevelopersAsync_ReturnsOkResultWithDevelopersList()
    {
        // Arrange
        var developersList = new List<DeveloperResponseDto>
        {
            new() { Id = Guid.NewGuid(), Username = "user1" },
            new() { Id = Guid.NewGuid(), Username = "user2" }
        };

        _developerServiceMock
            .Setup(s => s.ListDevelopersAsync())
            .ReturnsAsync(developersList);

        // Act
        var result = await _controller.ListDevelopersAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(developersList, okResult.Value);
        _developerServiceMock.Verify(s => s.ListDevelopersAsync(), Times.Once);
    }
}