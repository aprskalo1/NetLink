using Microsoft.AspNetCore.Mvc;
using Moq;
using NetLink.API.Controllers;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Services;

namespace NetLink.Tests.Controllers;

public class EndUsersControllerTests
{
    private readonly Mock<IEndUserService> _endUserServiceMock;
    private readonly EndUsersController _controller;

    public EndUsersControllerTests()
    {
        _endUserServiceMock = new Mock<IEndUserService>();
        _controller = new EndUsersController(_endUserServiceMock.Object);
    }

    [Fact]
    public async Task RegisterEndUserAsync_ValidModel_ReturnsOkResultWithId()
    {
        // Arrange
        var endUserRequestDto = new EndUserRequestDto { Username = "testuser" };
        const string devToken = "validToken";
        const string expectedEndUserId = "endUserId";
        _endUserServiceMock.Setup(s => s.RegisterEndUserAsync(endUserRequestDto, devToken)).ReturnsAsync(expectedEndUserId);

        // Act
        var result = await _controller.RegisterEndUserAsync(endUserRequestDto, devToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedEndUserId, okResult.Value);
        _endUserServiceMock.Verify(s => s.RegisterEndUserAsync(endUserRequestDto, devToken), Times.Once);
    }

    [Fact]
    public async Task RegisterEndUserAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Username", "Required");
        var endUserRequestDto = new EndUserRequestDto();

        // Act
        var result = await _controller.RegisterEndUserAsync(endUserRequestDto, "devToken");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
        _endUserServiceMock.Verify(s => s.RegisterEndUserAsync(It.IsAny<EndUserRequestDto>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task GetEndUserByIdAsync_ExistingId_ReturnsOkResultWithEndUser()
    {
        // Arrange
        var endUserId = "endUserId";
        var devToken = "devToken";
        var endUserResponseDto = new EndUserResponseDto { Id = endUserId, Username = "testuser" };
        _endUserServiceMock.Setup(s => s.GetEndUserByIdAsync(endUserId, devToken)).ReturnsAsync(endUserResponseDto);

        // Act
        var result = await _controller.GetEndUserByIdAsync(endUserId, devToken);

        // Assert
        var okResult = Assert.IsType<ActionResult<EndUserResponseDto>>(result);
        var objectResult = Assert.IsType<OkObjectResult>(okResult.Result);
        Assert.Equal(endUserResponseDto, objectResult.Value);
        _endUserServiceMock.Verify(s => s.GetEndUserByIdAsync(endUserId, devToken), Times.Once);
    }

    [Fact]
    public async Task ValidateEndUserAsync_ValidId_ReturnsOk()
    {
        // Arrange
        const string endUserId = "endUserId";
        _endUserServiceMock.Setup(s => s.ValidateEndUserAsync(endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.ValidateEndUserAsync(endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _endUserServiceMock.Verify(s => s.ValidateEndUserAsync(endUserId), Times.Once);
    }

    [Fact]
    public async Task ListDeveloperEndUsersAsync_ValidToken_ReturnsOkResultWithEndUsers()
    {
        // Arrange
        const string devToken = "devToken";
        var endUsersList = new List<EndUserResponseDto> { new() { Id = "endUserId", Username = "testuser" } };
        _endUserServiceMock.Setup(s => s.ListDevelopersEndUsersAsync(devToken)).ReturnsAsync(endUsersList);

        // Act
        var result = await _controller.ListDeveloperEndUsersAsync(devToken);

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<EndUserResponseDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(endUsersList, okResult.Value);
        _endUserServiceMock.Verify(s => s.ListDevelopersEndUsersAsync(devToken), Times.Once);
    }

    [Fact]
    public async Task DeactivateEndUserAsync_ValidId_ReturnsOk()
    {
        // Arrange
        const string endUserId = "endUserId";
        _endUserServiceMock.Setup(s => s.DeactivateEndUserAsync(endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeactivateEndUserAsync(endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _endUserServiceMock.Verify(s => s.DeactivateEndUserAsync(endUserId), Times.Once);
    }

    [Fact]
    public async Task ReactivateEndUserAsync_ValidId_ReturnsOk()
    {
        // Arrange
        const string endUserId = "endUserId";
        _endUserServiceMock.Setup(s => s.ReactivateEndUserAsync(endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.ReactivateEndUserAsync(endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _endUserServiceMock.Verify(s => s.ReactivateEndUserAsync(endUserId), Times.Once);
    }

    [Fact]
    public async Task SoftDeleteEndUserAsync_ValidId_ReturnsOk()
    {
        // Arrange
        const string endUserId = "endUserId";
        _endUserServiceMock.Setup(s => s.SoftDeleteEndUserAsync(endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.SoftDeleteEndUserAsync(endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _endUserServiceMock.Verify(s => s.SoftDeleteEndUserAsync(endUserId), Times.Once);
    }

    [Fact]
    public async Task RestoreEndUserAsync_ValidId_ReturnsOk()
    {
        // Arrange
        const string endUserId = "endUserId";
        _endUserServiceMock.Setup(s => s.RestoreEndUserAsync(endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RestoreEndUserAsync(endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _endUserServiceMock.Verify(s => s.RestoreEndUserAsync(endUserId), Times.Once);
    }

    [Fact]
    public async Task AssignSensorsToEndUserAsync_ValidRequest_ReturnsOk()
    {
        // Arrange
        const string endUserId = "endUserId";
        var sensorIds = new List<Guid> { Guid.NewGuid() };
        _endUserServiceMock.Setup(s => s.AssignSensorsToEndUserAsync(sensorIds, endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AssignSensorsToEndUserAsync(sensorIds, endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _endUserServiceMock.Verify(s => s.AssignSensorsToEndUserAsync(sensorIds, endUserId), Times.Once);
    }

    [Fact]
    public async Task ListEndUserSensorsAsync_ValidEndUserId_ReturnsOkResultWithSensors()
    {
        // Arrange
        const string endUserId = "endUserId";
        var sensorsList = new List<SensorResponseDto> { new() { Id = Guid.NewGuid() } };
        _endUserServiceMock.Setup(s => s.ListEndUserSensorsAsync(endUserId)).ReturnsAsync(sensorsList);

        // Act
        var result = await _controller.ListEndUserSensorsAsync(endUserId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<SensorResponseDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(sensorsList, okResult.Value);
        _endUserServiceMock.Verify(s => s.ListEndUserSensorsAsync(endUserId), Times.Once);
    }

    [Fact]
    public async Task ListPagedEndUserSensorsAsync_ValidRequest_ReturnsOkResultWithPagedSensors()
    {
        // Arrange
        const string endUserId = "endUserId";
        const int page = 1;
        const int pageSize = 10;
        var pagedSensors = new PagedSensorDto { Sensors = [], TotalCount = 1 };
        _endUserServiceMock.Setup(s => s.ListPagedEndUserSensorsAsync(endUserId, page, pageSize, null)).ReturnsAsync(pagedSensors);

        // Act
        var result = await _controller.ListPagedEndUserSensorsAsync(endUserId, page, pageSize);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PagedSensorDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(pagedSensors, okResult.Value);
        _endUserServiceMock.Verify(s => s.ListPagedEndUserSensorsAsync(endUserId, page, pageSize, null), Times.Once);
    }
}