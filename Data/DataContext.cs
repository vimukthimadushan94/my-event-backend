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

            builder.Entity<Event>()
                .HasOne(e => e.CreatedByUser) 
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Events)
                .WithMany(e => e.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserEvent",
                    j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId")
                );

            builder.Entity<EventItemUser>()
                .HasKey(eiu => new { eiu.EventItemId, eiu.UserId });

            builder.Entity<EventItemUser>()
                .HasOne(eiu => eiu.EventItem)
                .WithMany(ei => ei.EventItemUsers)
                .HasForeignKey(eiu => eiu.EventItemId);

            builder.Entity<EventItemUser>()
                .HasOne(eiu => eiu.ApplicationUser)
                .WithMany(u => u.EventItemUsers)
                .HasForeignKey(eiu => eiu.UserId);
        }

        public DbSet<Event> Events {  get; set; }

        public DbSet<EventItem> EventsItems { get; set; }

        public DbSet<EventItemUser> EventItemUsers { get; set; }

    }
}
