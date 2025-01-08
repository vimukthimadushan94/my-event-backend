using System.ComponentModel.DataAnnotations.Schema;

namespace my_event_backend.Models
{
    public class EventItemUser
    {
        public string EventItemId { get; set; }
        [ForeignKey("EventItemId")]
        public EventItem EventItem { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
