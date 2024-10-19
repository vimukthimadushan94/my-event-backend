using Microsoft.EntityFrameworkCore;
using my_event_backend.Models;

namespace my_event_backend.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) 
        {
            
        }

        public DbSet<Event> Events {  get; set; }
    }
}
