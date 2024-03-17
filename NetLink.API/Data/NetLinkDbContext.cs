using Microsoft.EntityFrameworkCore;
using NetLink.API.Models;

namespace NetLink.API.Data
{
    public class NetLinkDbContext : DbContext
    {
        public NetLinkDbContext(DbContextOptions<NetLinkDbContext> options) : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<DeveloperUser> DeveloperUsers { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorGroup> SensorGroups { get; set; }
        public DbSet<EndUserSensor> EndUserSensors { get; set; }
        public DbSet<EndUserSensorGroup> EndUserSensorGroups { get; set; }
        public DbSet<RecordedValue> RecordedValues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;Database=NetLink;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }
    }
}
