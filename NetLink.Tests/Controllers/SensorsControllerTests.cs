using Microsoft.AspNetCore.Mvc;
using Moq;
using NetLink.API.Controllers;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Services;

namespace NetLink.Tests.Controllers;

public class SensorsControllerTests
{
    private readonly Mock<ISensorOperationsService> _sensorServiceMock;
    private readonly SensorsController _controller;

    public SensorsControllerTests()
    {
        _sensorServiceMock = new Mock<ISensorOperationsService>();
        _controller = new SensorsController(_sensorServiceMock.Object);
    }

    [Fact]
    public async Task AddSensorAsync_ValidModel_ReturnsOkWithSensorId()
    {
        // Arrange
        var sensorRequestDto = new SensorRequestDto { DeviceName = "TestSensor" };
        const string endUserId = "endUserId";
        var expectedSensorId = Guid.NewGuid();
        _sensorServiceMock.Setup(s => s.AddSensorAsync(sensorRequestDto, endUserId)).ReturnsAsync(expectedSensorId);

        // Act
        var result = await _controller.AddSensorAsync(sensorRequestDto, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedSensorId, okResult.Value);
        _sensorServiceMock.Verify(s => s.AddSensorAsync(sensorRequestDto, endUserId), Times.Once);
    }

    [Fact]
    public async Task AddSensorAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("DeviceName", "Required");
        var sensorRequestDto = new SensorRequestDto();
        const string endUserId = "endUserId";

        // Act
        var result = await _controller.AddSensorAsync(sensorRequestDto, endUserId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        _sensorServiceMock.Verify(s => s.AddSensorAsync(It.IsAny<SensorRequestDto>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task GetSensorByNameAsync_ExistingSensor_ReturnsOkWithSensor()
    {
        // Arrange
        const string deviceName = "TestSensor";
        const string endUserId = "endUserId";
        var sensorResponseDto = new SensorResponseDto { Id = Guid.NewGuid(), DeviceName = deviceName };
        _sensorServiceMock.Setup(s => s.GetSensorByNameAsync(deviceName, endUserId)).ReturnsAsync(sensorResponseDto);

        // Act
        var result = await _controller.GetSensorByNameAsync(deviceName, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(sensorResponseDto, okResult.Value);
        _sensorServiceMock.Verify(s => s.GetSensorByNameAsync(deviceName, endUserId), Times.Once);
    }

    [Fact]
    public async Task GetSensorByIdAsync_ExistingSensor_ReturnsOkWithSensor()
    {
        // Arrange
        var sensorId = Guid.NewGuid();
        const string endUserId = "endUserId";
        var sensorResponseDto = new SensorResponseDto { Id = sensorId, DeviceName = "TestSensor" };
        _sensorServiceMock.Setup(s => s.GetSensorByIdAsync(sensorId, endUserId)).ReturnsAsync(sensorResponseDto);

        // Act
        var result = await _controller.GetSensorByIdAsync(sensorId, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(sensorResponseDto, okResult.Value);
        _sensorServiceMock.Verify(s => s.GetSensorByIdAsync(sensorId, endUserId), Times.Once);
    }

    [Fact]
    public async Task GetSensorsFromGroupAsync_ValidRequest_ReturnsOkWithSensorsList()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        const string endUserId = "endUserId";
        var sensorsList = new List<SensorResponseDto>
        {
            new SensorResponseDto { Id = Guid.NewGuid(), DeviceName = "Sensor1" },
            new SensorResponseDto { Id = Guid.NewGuid(), DeviceName = "Sensor2" }
        };
        _sensorServiceMock.Setup(s => s.GetSensorsFromGroupAsync(groupId, endUserId)).ReturnsAsync(sensorsList);

        // Act
        var result = await _controller.GetSensorsFromGroupAsync(groupId, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(sensorsList, okResult.Value);
        _sensorServiceMock.Verify(s => s.GetSensorsFromGroupAsync(groupId, endUserId), Times.Once);
    }

    [Fact]
    public async Task UpdateSensorAsync_ValidModel_ReturnsOkWithUpdatedSensor()
    {
        // Arrange
        var sensorId = Guid.NewGuid();
        var sensorRequestDto = new SensorRequestDto { DeviceName = "UpdatedSensor" };
        const string endUserId = "endUserId";
        var updatedSensorResponseDto = new SensorResponseDto { Id = sensorId, DeviceName = "UpdatedSensor" };
        _sensorServiceMock.Setup(s => s.UpdateSensorAsync(sensorId, sensorRequestDto, endUserId)).ReturnsAsync(updatedSensorResponseDto);

        // Act
        var result = await _controller.UpdateSensorAsync(sensorId, sensorRequestDto, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(updatedSensorResponseDto, okResult.Value);
        _sensorServiceMock.Verify(s => s.UpdateSensorAsync(sensorId, sensorRequestDto, endUserId), Times.Once);
    }

    [Fact]
    public async Task UpdateSensorAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("DeviceName", "Required");
        var sensorRequestDto = new SensorRequestDto();
        var sensorId = Guid.NewGuid();
        const string endUserId = "endUserId";

        // Act
        var result = await _controller.UpdateSensorAsync(sensorId, sensorRequestDto, endUserId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        _sensorServiceMock.Verify(s => s.UpdateSensorAsync(It.IsAny<Guid>(), It.IsAny<SensorRequestDto>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task DeleteSensorAsync_ValidSensorId_ReturnsOk()
    {
        // Arrange
        var sensorId = Guid.NewGuid();
        const string endUserId = "endUserId";
        _sensorServiceMock.Setup(s => s.DeleteSensorAsync(sensorId, endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteSensorAsync(sensorId, endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _sensorServiceMock.Verify(s => s.DeleteSensorAsync(sensorId, endUserId), Times.Once);
    }
}