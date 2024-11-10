using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs.Request;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SensorsController(ISensorOperationsService sensorService) : ControllerBase
{
    [HttpPost("AddSensor")]
    public async Task<ActionResult> AddSensorAsync(SensorRequestDto sensorRequestDto, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await sensorService.AddSensorAsync(sensorRequestDto, endUserId));
    }

    [HttpGet("GetSensorByName")]
    public async Task<ActionResult> GetSensorByNameAsync(string deviceName, string endUserId)
    {
        return Ok(await sensorService.GetSensorByNameAsync(deviceName, endUserId));
    }

    [HttpGet("GetSensorById")]
    public async Task<ActionResult> GetSensorByIdAsync(Guid sensorId, string endUserId)
    {
        return Ok(await sensorService.GetSensorByIdAsync(sensorId, endUserId));
    }

    [HttpGet("GetSensorsFromGroup")]
    public async Task<ActionResult> GetSensorsFromGroupAsync(Guid groupId, string endUserId)
    {
        return Ok(await sensorService.GetSensorsFromGroupAsync(groupId, endUserId));
    }

    [HttpPut("UpdateSensor")]
    public async Task<ActionResult> UpdateSensorAsync(Guid sensorId, SensorRequestDto sensorRequestDto,
        string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await sensorService.UpdateSensorAsync(sensorId, sensorRequestDto, endUserId));
    }

    [HttpDelete("DeleteSensor")]
    public async Task<ActionResult> DeleteSensorAsync(Guid sensorId, string endUserId)
    {
        await sensorService.DeleteSensorAsync(sensorId, endUserId);
        return Ok();
    }
}