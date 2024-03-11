using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Services;

namespace NetLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;


        public SensorsController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        // GET: api/Sensors
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Sensor>>> GetSensors()
        //{
        //    return await _context.Sensors.ToListAsync();
        //}

        //GET: api/Sensors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDTO>> GetSensor(Guid id)
        {
            try
            {
                return Ok(await _sensorService.GetSensorByIdAsync(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Sensors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSensor(Guid id, Sensor sensor)
        //{
        //    if (id != sensor.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(sensor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SensorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Sensors
        [HttpPost]
        public async Task<ActionResult> AddSensorAsync(SensorDTO sensorDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var sensorId = await _sensorService.AddSensorAsync(sensorDTO);

            return Ok(sensorId);
        }

        // DELETE: api/Sensors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSensor(Guid id)
        //{
        //    var sensor = await _context.Sensors.FindAsync(id);
        //    if (sensor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Sensors.Remove(sensor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SensorExists(Guid id)
        //{
        //    return _context.Sensors.Any(e => e.Id == id);
        //}
    }
}
