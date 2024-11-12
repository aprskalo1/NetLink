using Microsoft.AspNetCore.Mvc;
using Moq;
using NetLink.API.Controllers;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Services;

namespace NetLink.Tests.Controllers;

public class RecordedValuesControllerTests
{
    private readonly Mock<ISensorOperationsService> _sensorServiceMock;
    private readonly RecordedValuesController _controller;

    public RecordedValuesControllerTests()
    {
        _sensorServiceMock = new Mock<ISensorOperationsService>();
        _controller = new RecordedValuesController(_sensorServiceMock.Object);
    }

    [Fact]
    public async Task RecordValueBySensorNameAsync_ValidModel_ReturnsOk()
    {
        // Arrange
        var recordedValueRequestDto = new RecordedValueRequestDto { Value = 25.5 };
        const string sensorName = "TestSensor";
        const string endUserId = "endUserId";
        _sensorServiceMock.Setup(s => s.RecordValueByNameAsync(recordedValueRequestDto, sensorName, endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RecordValueBySensorNameAsync(recordedValueRequestDto, sensorName, endUserId);

        // Assert
        Assert.IsType<OkResult>(result.Result);
        _sensorServiceMock.Verify(s => s.RecordValueByNameAsync(recordedValueRequestDto, sensorName, endUserId), Times.Once);
    }

    [Fact]
    public async Task RecordValueBySensorNameAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Value", "Required");
        var recordedValueRequestDto = new RecordedValueRequestDto();
        const string sensorName = "TestSensor";
        const string endUserId = "endUserId";

        // Act
        var result = await _controller.RecordValueBySensorNameAsync(recordedValueRequestDto, sensorName, endUserId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        _sensorServiceMock.Verify(s => s.RecordValueByNameAsync(It.IsAny<RecordedValueRequestDto>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task RecordValueBySensorIdAsync_ValidModel_ReturnsOk()
    {
        // Arrange
        var recordedValueRequestDto = new RecordedValueRequestDto { Value = 25.5 };
        var sensorId = Guid.NewGuid();
        _sensorServiceMock.Setup(s => s.RecordValueByIdAsync(recordedValueRequestDto, sensorId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RecordValueBySensorIdAsync(recordedValueRequestDto, sensorId);

        // Assert
        Assert.IsType<OkResult>(result.Result);
        _sensorServiceMock.Verify(s => s.RecordValueByIdAsync(recordedValueRequestDto, sensorId), Times.Once);
    }

    [Fact]
    public async Task RecordValueBySensorIdAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Value", "Required");
        var recordedValueRequestDto = new RecordedValueRequestDto();
        var sensorId = Guid.NewGuid();

        // Act
        var result = await _controller.RecordValueBySensorIdAsync(recordedValueRequestDto, sensorId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        _sensorServiceMock.Verify(s => s.RecordValueByIdAsync(It.IsAny<RecordedValueRequestDto>(), It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task GetRecordedValuesAsync_ValidRequest_ReturnsOkResultWithRecordedValues()
    {
        // Arrange
        var sensorId = Guid.NewGuid();
        const string endUserId = "endUserId";
        var recordedValues = new List<RecordedValueResponseDto>
        {
            new() { Value = 25.5 },
            new() { Value = 30.2 }
        };
        _sensorServiceMock
            .Setup(s => s.GetRecordedValuesAsync(sensorId, endUserId, null, false, null, null))
            .ReturnsAsync(recordedValues);

        // Act
        var result = await _controller.GetRecordedValuesAsync(sensorId, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(recordedValues, okResult.Value);
        _sensorServiceMock.Verify(s => s.GetRecordedValuesAsync(sensorId, endUserId, null, false, null, null), Times.Once);
    }

    [Fact]
    public async Task GetRecordedValuesAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("sensorId", "Required");
        var sensorId = Guid.NewGuid();
        const string endUserId = "endUserId";

        // Act
        var result = await _controller.GetRecordedValuesAsync(sensorId, endUserId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        _sensorServiceMock.Verify(
            s => s.GetRecordedValuesAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<bool>(), It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>()), Times.Never);
    }

    [Fact]
    public async Task RecordValueRemotelyAsync_ValidModel_ReturnsOk()
    {
        // Arrange
        var recordedValueRequestDto = new RecordedValueRequestDto { Value = 25.5 };
        var sensorId = Guid.NewGuid();
        _sensorServiceMock.Setup(s => s.RecordValueRemotelyAsync(recordedValueRequestDto, sensorId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RecordValueRemotelyAsync(recordedValueRequestDto, sensorId);

        // Assert
        Assert.IsType<OkResult>(result.Result);
        _sensorServiceMock.Verify(s => s.RecordValueRemotelyAsync(recordedValueRequestDto, sensorId), Times.Once);
    }

    [Fact]
    public async Task RecordValueRemotelyAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Value", "Required");
        var recordedValueRequestDto = new RecordedValueRequestDto();
        var sensorId = Guid.NewGuid();

        // Act
        var result = await _controller.RecordValueRemotelyAsync(recordedValueRequestDto, sensorId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        _sensorServiceMock.Verify(s => s.RecordValueRemotelyAsync(It.IsAny<RecordedValueRequestDto>(), It.IsAny<Guid>()), Times.Never);
    }
}