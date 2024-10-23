using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace my_event_backend.Models
{
    public class User : IdentityUser
    {
        public List<Event> Events { get; set; }

    }
}
