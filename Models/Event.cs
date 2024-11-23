using Azure;
using Microsoft.AspNetCore.Identity;

namespace my_event_backend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty ;

        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }

        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
