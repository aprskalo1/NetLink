using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Models;
using NetLink.API.Services;

namespace NetLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDevTokenService _tokenService;

        public DevelopersController(IDevTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // GET: api/Developers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
        //{
        //    return await _context.Developers.ToListAsync();
        //}

        // GET: api/Developers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Developer>> GetDeveloper(Guid id)
        //{
        //    var developer = await _context.Developers.FindAsync(id);

        //    if (developer == null)
        //    {
        //        return NotFound();
        //    }

        //    return developer;
        //}

        // PUT: api/Developers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDeveloper(Guid id, Developer developer)
        //{
        //    if (id != developer.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(developer).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DeveloperExists(id))
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

        // POST: api/Developers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddDeveloperAsync(DeveloperDTO developerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var developerId = await _tokenService.AddTokenAsync(developerDTO);
            return Ok(developerId);
        }

        [HttpGet("checkIfTokenExists")]
        public async Task<IActionResult> CheckTokenExistsAsync(string token)
        {
            var devToken = await _tokenService.CheckIfTokenExistsAsync(token);
            return devToken ? Ok(new { exists = devToken }) : NotFound(new { exists = devToken });
        }

        //// DELETE: api/Developers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDeveloper(Guid id)
        //{
        //    var developer = await _context.Developers.FindAsync(id);
        //    if (developer == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Developers.Remove(developer);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool DeveloperExists(Guid id)
        //{
        //    return _context.Developers.Any(e => e.Id == id);
        //}
    }
}
