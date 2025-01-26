using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace my_event_backend.Models
{
    [Keyless]
    public class EventItemUser
    {
        [ForeignKey("UserId")]
        public string UserId { get; set; }


        [ForeignKey("EventItemId")]
        public string EventItemId { get; set; }
       
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public EventItem EventItem { get; set; } = null!;

    }
}
