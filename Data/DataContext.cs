using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using my_event_backend.Models;

namespace my_event_backend.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the one-to-many relationship for CreatedByUser
            builder.Entity<Event>()
                .HasOne(e => e.CreatedByUser) // Event.CreatedByUser
                .WithMany() // No navigation property in ApplicationUser for this relationship
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes if needed

            // Configure the many-to-many relationship
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Events)
                .WithMany(e => e.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserEvent", // Join table name
                    j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId")
                );
        }

        public DbSet<Event> Events {  get; set; }

        public DbSet<EventItem> EventsItems { get; set; }

    }
}
