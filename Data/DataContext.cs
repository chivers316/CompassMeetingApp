using CompassMeetingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CompassMeetingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<MeetingRoom> MeetingRooms => Set<MeetingRoom>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
