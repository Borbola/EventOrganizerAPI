using EventOrganizerAPI.Context;
using EventOrganizerAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventOrganizerAPI.DTO;

namespace EventOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(EventOrganizerContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var events = await context.Events.ToListAsync();
            if (events is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evnt = await context.Events.SingleOrDefaultAsync(x => x.Id == id);
            if (evnt is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(evnt);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventDTO eventDTO)
        {
            var evnt = new Event
            {
                ParticipantId = eventDTO.ParticipantId,
                LocationId = eventDTO.LocationId,
                EventStart = eventDTO.EventStart,
                EventEnd = eventDTO.EventEnd,
            };
            if (evnt is null) return BadRequest("hiba");

            context.Events.Add(evnt);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = evnt.Id }, evnt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Event evnt)
        {
            if (evnt is null) return BadRequest("eh");

            var eventToUpdate = await context.Events.SingleOrDefaultAsync(x => x.Id == id);
            if (eventToUpdate is null) return BadRequest("hiba történt a módosítás során");

            eventToUpdate.ParticipantId = evnt.ParticipantId;
            eventToUpdate.LocationId = evnt.LocationId;
            eventToUpdate.EventStart = evnt.EventStart;
            eventToUpdate.EventEnd = evnt.EventEnd;
            await context.SaveChangesAsync();
            return Ok("sikeres módosítás");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rentalToDelete = await context.Events.SingleOrDefaultAsync(x => x.Id == id);
            if (rentalToDelete is null) return BadRequest("valami hiba történt");

            context.Events.Remove(rentalToDelete);
            await context.SaveChangesAsync();
            return Ok("A törlés sikeres volt");
        }
    }
}

