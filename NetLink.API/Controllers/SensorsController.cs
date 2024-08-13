using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs;
using NetLink.API.Exceptions;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorsController(ISensorService sensorService) : ControllerBase
{
    [HttpGet("GetSensorById")]
    public async Task<ActionResult<SensorDto>> GetSensorByNameAsync(string deviceName, string endUserId)
    {
        try //TODO:global exception handling
        {
            return Ok(await sensorService.GetSensorByNameAsync(deviceName, endUserId));
        }
        catch (NotFoundException)
        {
            throw new NotFoundException("Sensor not found.");
        }
    }
        
    [HttpPost("AddSensor")]
    public async Task<ActionResult> AddSensorAsync(SensorDto sensorDto, string endUserId)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var sensor = await sensorService.AddSensorAsync(sensorDto, endUserId);

        return Ok(sensor);
    }
}