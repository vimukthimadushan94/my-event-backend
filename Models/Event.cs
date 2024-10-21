using Azure;

namespace my_event_backend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty ;
        public List<User> Users { get; } = [];
    }
}
