using Microsoft.AspNetCore.Mvc;
using Moq;
using NetLink.API.Controllers;
using NetLink.API.DTOs.Request;
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
    public async Task AddDeveloperAsync_ValidRequest_ReturnsOk()
    {
        var developerRequest = new DeveloperRequestDto
        {
            Username = "test"
        };

        // Mock the method to return a valid Guid instead of a bool
        var mockDeveloperId = Guid.NewGuid();
        _developerServiceMock.Setup(service => service.AddDeveloperAsync(developerRequest))
            .ReturnsAsync(mockDeveloperId);

        // Act
        var result = await _controller.AddDeveloperAsync(developerRequest);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(mockDeveloperId, okResult.Value); // Ensure the expected Guid is returned
        _developerServiceMock.Verify(s => s.AddDeveloperAsync(developerRequest), Times.Once);
    }
}