using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace my_event_backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicturePath { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();

        public List<EventItemUser> EventItemUsers { get; set; } = new List<EventItemUser>();

    }
}
