using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_event_backend.Data;
using my_event_backend.Models;

namespace my_event_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context) { 
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
         
            var events = await _context.Events
                .Include(u => u.Users)
                .Include(e => e.CreatedByUser)
                .ToListAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Event>>> GetEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if(eventItem is null)
                return NotFound("Event not found");
            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<ActionResult<List<Event>>> CreateEvent(Event eventItem)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            eventItem.CreatedByUserId = userId;
            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync();
            return Ok(await _context.Events.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Event>>> updateEvent(Event updatedEvent)
        {
            var dbEvent = await _context.Events.FindAsync(updatedEvent.Id);
            if (dbEvent is null)
                return NotFound("The Event not found");

            dbEvent.Id = updatedEvent.Id;
            dbEvent.Name = updatedEvent.Name;
            dbEvent.Color = updatedEvent.Color;
            dbEvent.Description = updatedEvent.Description;

            await _context.SaveChangesAsync();

            return Ok(await _context.Events.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Event>>> DeleteEvent(int id)
        {
            var dbEvent = await _context.Events.FindAsync(id);
            if (dbEvent is null)
                return NotFound("Event not found");

            _context.Events.Remove(dbEvent);
            await _context.SaveChangesAsync();

            return Ok(await _context.Events.ToListAsync());

        }

        //get event items related to the specific event
        [HttpGet("/api/{id}/eventItems")]
        public async Task<ActionResult<EventItem>> GetEventItemsByEventId(int id)
        {
            var eventItems = await _context.EventsItems
                .Where(x => x.EventId == id)
                .ToListAsync();
            return Ok(eventItems);
        }
    }
}
