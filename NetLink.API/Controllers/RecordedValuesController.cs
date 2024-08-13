using Microsoft.AspNetCore.Mvc;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecordedValuesController : ControllerBase
{
    private readonly ISensorService _sensorService;

    public RecordedValuesController(NetLinkDbContext context, ISensorService sensorService)
    {
        _sensorService = sensorService;
    }
        
    [HttpPost("AddRecordedValue")]
    public async Task<ActionResult<RecordedValueDto>> PostRecordedValue(RecordedValueDto recordedValueDto, string sensorName, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var recordedValue = await _sensorService.AddRecordedValueAsync(recordedValueDto, sensorName, endUserId);

        return recordedValue;
    }
}