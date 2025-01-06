using my_event_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace my_event_backend.Dtos
{
    public class EventItemUpdateDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public required DateTime From { get; set; }

        public required DateTime To { get; set; }

        public int Price { get; set; }

        public string Location { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }
}
