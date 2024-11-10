using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs.Request;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupingController(IGroupingService groupingService) : ControllerBase
{
    [HttpPost("CreateGroup")]
    public async Task<ActionResult> CreateGroupAsync(GroupRequestDto groupRequestDto, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await groupingService.CreateGroupAsync(groupRequestDto, endUserId));
    }

    [HttpPost("AddSensorToGroup")]
    public async Task<ActionResult> AddSensorToGroupAsync(Guid groupId, Guid sensorId, string endUserId)
    {
        await groupingService.AddSensorToGroupAsync(groupId, sensorId, endUserId);
        return Ok();
    }

    [HttpPost("AddSensorsToGroup")]
    public async Task<ActionResult> AddSensorsToGroupAsync(Guid groupId, List<Guid> sensorIds, string endUserId)
    {
        await groupingService.AddSensorsToGroupAsync(groupId, sensorIds, endUserId);
        return Ok();
    }

    [HttpPost("RemoveSensorFromGroup")]
    public async Task<ActionResult> RemoveSensorFromGroupAsync(Guid groupId, Guid sensorId, string endUserId)
    {
        await groupingService.RemoveSensorFromGroupAsync(groupId, sensorId, endUserId);
        return Ok();
    }

    [HttpDelete("DeleteGroup")]
    public async Task<ActionResult> DeleteGroupAsync(Guid groupId, string endUserId)
    {
        await groupingService.DeleteGroupAsync(groupId, endUserId);
        return Ok();
    }

    [HttpGet("GetEndUserGroups")]
    public async Task<ActionResult> GetEndUserGroupsAsync(string endUserId)
    {
        return Ok(await groupingService.GetEndUserGroupsAsync(endUserId));
    }

    [HttpGet("GetGroupById")]
    public async Task<ActionResult> GetGroupByIdAsync(Guid groupId, string endUserId)
    {
        return Ok(await groupingService.GetGroupByIdAsync(groupId, endUserId));
    }

    [HttpPut("UpdateGroup")]
    public async Task<ActionResult> UpdateGroupAsync(GroupRequestDto groupRequestDto, Guid groupId, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await groupingService.UpdateGroupAsync(groupRequestDto, groupId, endUserId));
    }
}