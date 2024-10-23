using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using my_event_backend.Models;

namespace my_event_backend.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events {  get; set; }

    }
}
