using EventOrganizerAPI.Context;
using EventOrganizerAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController(EventOrganizerContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var participant = await context.Participants.ToListAsync();
            if (participant is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(participant);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var participant = await context.Participants.SingleOrDefaultAsync(x => x.Id == id);
            if (participant is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(participant);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Participant participant)
        {
            if (participant is null) return BadRequest("hiba");

            context.Participants.Add(participant);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = participant.Id }, participant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Participant participant)
        {
            if (participant is null) return BadRequest("eh");

            var participantToUpdate = await context.Participants.SingleOrDefaultAsync(x => x.Id == id);
            if (participantToUpdate is null) return BadRequest("hiba történt a módosítás során");

            participantToUpdate.Name = participant.Name;
            participantToUpdate.Email = participant.Email;
            participantToUpdate.PhoneNumber = participant.PhoneNumber;
            await context.SaveChangesAsync();
            return Ok("sikeres módosítás");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var participantToDelete = await context.Participants.SingleOrDefaultAsync(x => x.Id == id);
            if (participantToDelete is null) return BadRequest("valami hiba történt");

            context.Participants.Remove(participantToDelete);
            await context.SaveChangesAsync();
            return Ok("A törlés sikeres volt");
        }
    }
}
