using Microsoft.AspNetCore.Mvc;
using Moq;
using NetLink.API.Controllers;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Services;

namespace NetLink.Tests.Controllers;

public class GroupingControllerTests
{
    private readonly Mock<IGroupingService> _groupingServiceMock;
    private readonly GroupingController _controller;

    public GroupingControllerTests()
    {
        _groupingServiceMock = new Mock<IGroupingService>();
        _controller = new GroupingController(_groupingServiceMock.Object);
    }

    [Fact]
    public async Task CreateGroupAsync_ValidModel_ReturnsOkResultWithGuid()
    {
        // Arrange
        var groupRequestDto = new GroupRequestDto { GroupName = "Test Group" };
        const string endUserId = "endUserId";
        var expectedGroupId = Guid.NewGuid();
        _groupingServiceMock.Setup(s => s.CreateGroupAsync(groupRequestDto, endUserId)).ReturnsAsync(expectedGroupId);

        // Act
        var result = await _controller.CreateGroupAsync(groupRequestDto, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedGroupId, okResult.Value);
        _groupingServiceMock.Verify(s => s.CreateGroupAsync(groupRequestDto, endUserId), Times.Once);
    }

    [Fact]
    public async Task CreateGroupAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Required");
        var groupRequestDto = new GroupRequestDto();
        const string endUserId = "endUserId";

        // Act
        var result = await _controller.CreateGroupAsync(groupRequestDto, endUserId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
        _groupingServiceMock.Verify(s => s.CreateGroupAsync(It.IsAny<GroupRequestDto>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task AddSensorToGroupAsync_ValidRequest_ReturnsOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var sensorId = Guid.NewGuid();
        const string endUserId = "endUserId";
        _groupingServiceMock.Setup(s => s.AddSensorToGroupAsync(groupId, sensorId, endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddSensorToGroupAsync(groupId, sensorId, endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _groupingServiceMock.Verify(s => s.AddSensorToGroupAsync(groupId, sensorId, endUserId), Times.Once);
    }

    [Fact]
    public async Task AddSensorsToGroupAsync_ValidRequest_ReturnsOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var sensorIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        const string endUserId = "endUserId";
        _groupingServiceMock.Setup(s => s.AddSensorsToGroupAsync(groupId, sensorIds, endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddSensorsToGroupAsync(groupId, sensorIds, endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _groupingServiceMock.Verify(s => s.AddSensorsToGroupAsync(groupId, sensorIds, endUserId), Times.Once);
    }

    [Fact]
    public async Task RemoveSensorFromGroupAsync_ValidRequest_ReturnsOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var sensorId = Guid.NewGuid();
        const string endUserId = "endUserId";
        _groupingServiceMock.Setup(s => s.RemoveSensorFromGroupAsync(groupId, sensorId, endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RemoveSensorFromGroupAsync(groupId, sensorId, endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _groupingServiceMock.Verify(s => s.RemoveSensorFromGroupAsync(groupId, sensorId, endUserId), Times.Once);
    }

    [Fact]
    public async Task DeleteGroupAsync_ValidRequest_ReturnsOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        const string endUserId = "endUserId";
        _groupingServiceMock.Setup(s => s.DeleteGroupAsync(groupId, endUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteGroupAsync(groupId, endUserId);

        // Assert
        Assert.IsType<OkResult>(result);
        _groupingServiceMock.Verify(s => s.DeleteGroupAsync(groupId, endUserId), Times.Once);
    }

    [Fact]
    public async Task GetEndUserGroupsAsync_ValidEndUserId_ReturnsOkResultWithGroups()
    {
        // Arrange
        const string endUserId = "endUserId";
        var groupsList = new List<GroupResponseDto> { new() { Id = Guid.NewGuid(), GroupName = "Group1" } };
        _groupingServiceMock.Setup(s => s.GetEndUserGroupsAsync(endUserId)).ReturnsAsync(groupsList);

        // Act
        var result = await _controller.GetEndUserGroupsAsync(endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(groupsList, okResult.Value);
        _groupingServiceMock.Verify(s => s.GetEndUserGroupsAsync(endUserId), Times.Once);
    }

    [Fact]
    public async Task GetGroupByIdAsync_ExistingId_ReturnsOkResultWithGroup()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        const string endUserId = "endUserId";
        var groupResponseDto = new GroupResponseDto { Id = groupId, GroupName = "Test Group" };
        _groupingServiceMock.Setup(s => s.GetGroupByIdAsync(groupId, endUserId)).ReturnsAsync(groupResponseDto);

        // Act
        var result = await _controller.GetGroupByIdAsync(groupId, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(groupResponseDto, okResult.Value);
        _groupingServiceMock.Verify(s => s.GetGroupByIdAsync(groupId, endUserId), Times.Once);
    }

    [Fact]
    public async Task UpdateGroupAsync_ValidModel_ReturnsOkResultWithUpdatedGroup()
    {
        // Arrange
        var groupRequestDto = new GroupRequestDto { GroupName = "Updated Group" };
        var groupId = Guid.NewGuid();
        const string endUserId = "endUserId";
        var updatedGroup = new GroupResponseDto { Id = groupId, GroupName = groupRequestDto.GroupName };
        _groupingServiceMock.Setup(s => s.UpdateGroupAsync(groupRequestDto, groupId, endUserId)).ReturnsAsync(updatedGroup);

        // Act
        var result = await _controller.UpdateGroupAsync(groupRequestDto, groupId, endUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(updatedGroup, okResult.Value);
        _groupingServiceMock.Verify(s => s.UpdateGroupAsync(groupRequestDto, groupId, endUserId), Times.Once);
    }

    [Fact]
    public async Task UpdateGroupAsync_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Required");
        var groupRequestDto = new GroupRequestDto();
        var groupId = Guid.NewGuid();
        const string endUserId = "endUserId";

        // Act
        var result = await _controller.UpdateGroupAsync(groupRequestDto, groupId, endUserId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        _groupingServiceMock.Verify(s => s.UpdateGroupAsync(It.IsAny<GroupRequestDto>(), It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
    }
}