using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.Services;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SensorsController(ISensorOperationsService sensorService) : ControllerBase
{
    [HttpGet("GetSensorByName")]
    public async Task<ActionResult> GetSensorByNameAsync(string deviceName, string endUserId)
    {
        return Ok(await sensorService.GetSensorByNameAsync(deviceName, endUserId)); //TODO: sensor response dto should have his id
    }

    [HttpPost("AddSensor")]
    public async Task<ActionResult> AddSensorAsync(SensorDto sensorDto, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await sensorService.AddSensorAsync(sensorDto, endUserId));
    }
}