using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using my_event_backend.Data;
using my_event_backend.Dtos;
using my_event_backend.Models;

namespace my_event_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EventItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventItem>>> GetEventsItems()
        {
            return await _context.EventsItems.ToListAsync();
        }

        // GET: api/EventItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventItem>> GetEventItem(string id)
        {
            var eventItem = await _context.EventsItems
                .Include(i => i.Event)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return eventItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventItem(string id, EventItem updateEventItem)
        {
            var dbEvent = await _context.EventsItems
                               .Include(e => e.Event)
                               .FirstOrDefaultAsync(e => e.Id == id);
            if (dbEvent is null)
                return NotFound("The Event not found");

            dbEvent.Name = updateEventItem.Name;
            dbEvent.Description = updateEventItem.Description;
            dbEvent.From = updateEventItem.From;
            dbEvent.To = updateEventItem.To;
            dbEvent.Price = updateEventItem.Price;
            dbEvent.Location = updateEventItem.Location;
            dbEvent.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            var responseDto = new EventItemUpdateDto
            {
                Name = dbEvent.Name,
                Description = dbEvent.Description,
                From = dbEvent.From,
                To = dbEvent.To,
                Price = (int)dbEvent.Price,
                Location = dbEvent.Location,
                Event = dbEvent.Event
            };

            return Ok(responseDto);
        }

        // POST: api/EventItems
        [HttpPost]
        public async Task<ActionResult<EventItem>> PostEventItem([FromBody] EventItemDto eventItemDto)
        {
            Console.WriteLine(eventItemDto);
            var eventItem = new EventItem
            {
                EventId = eventItemDto.EventId,
                Name = eventItemDto.Name,
                Description = eventItemDto.Description,
                From = eventItemDto.From,
                To = eventItemDto.To,
                Location = eventItemDto.Location,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.EventsItems.Add(eventItem);

            try
            {
                await _context.SaveChangesAsync();
                var participantIds = eventItemDto.Users
                                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(id => id.Trim())
                                    .ToList();

                foreach (var userId in participantIds)
                {
                    var eventItemUser = new EventItemUser
                    {
                        EventItemId = eventItem.Id,
                        UserId = userId
                    };
                    _context.EventItemUsers.Add(eventItemUser);
                }

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (EventItemExists(eventItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEventItem", new { id = eventItem.Id }, new EventItemDto
            {
                EventId = eventItem.EventId,
                Name = eventItem.Name
            });
        }

        // DELETE: api/EventItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventItem(string id)
        {
            var eventItem = await _context.EventsItems.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            _context.EventsItems.Remove(eventItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventItemExists(string id)
        {
            return _context.EventsItems.Any(e => e.Id == id);
        }
    }
}
