using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace my_event_backend.Models
{
    public class EventItem
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime From  { get; set; }
        public DateTime To { get;set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public string Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public Event? Event { get; set; }
    }
}
