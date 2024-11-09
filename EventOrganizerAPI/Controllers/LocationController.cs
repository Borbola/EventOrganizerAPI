using EventOrganizerAPI.Context;
using EventOrganizerAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController(EventOrganizerContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var locations = await context.Locations.ToListAsync();
            if (locations is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var location = await context.Locations.SingleOrDefaultAsync(x => x.Id == id);
            if (location is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Location location)
        {
            if (location is null) return BadRequest("hiba");

            context.Locations.Add(location);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Location location)
        {
            if (location is null) return BadRequest("eh");

            var locationToUpdate = await context.Locations.SingleOrDefaultAsync(x => x.Id == id);
            if (locationToUpdate is null) return BadRequest("hiba történt a módosítás során");

            locationToUpdate.City = location.City;
            locationToUpdate.EventType = location.EventType;
            await context.SaveChangesAsync();
            return Ok("sikeres módosítás");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var locationToDelete = await context.Locations.SingleOrDefaultAsync(x => x.Id == id);
            if (locationToDelete is null) return BadRequest("valami hiba történt");

            context.Locations.Remove(locationToDelete);
            await context.SaveChangesAsync();
            return Ok("A törlés sikeres volt");
        }
    }
}

