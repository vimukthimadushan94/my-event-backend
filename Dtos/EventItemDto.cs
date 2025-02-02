using my_event_backend.Models;

namespace my_event_backend.Dtos
{
    public class EventItemDto
    {
        public string? Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Location { get; set; }

        public string Users { get; set; }
        public List<UserDto> EventItemUsers { get; set; } = new List<UserDto>();
    }
}
