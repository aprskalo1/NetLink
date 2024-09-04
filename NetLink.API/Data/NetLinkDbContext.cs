using Microsoft.EntityFrameworkCore;
using NetLink.API.Models;

namespace NetLink.API.Data;

public class NetLinkDbContext(DbContextOptions<NetLinkDbContext> options) : DbContext(options)
{
    public DbSet<Developer> Developers { get; set; }
    public DbSet<EndUser> EndUsers { get; set; }
    public DbSet<DeveloperUser> DeveloperUsers { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<EndUserSensor> EndUserSensors { get; set; }
    public DbSet<EndUserGroup> EndUserGroups { get; set; }
    public DbSet<RecordedValue> RecordedValues { get; set; }
    public DbSet<SensorGroup> SensorGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=.;Database=NetLink;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SensorGroup>()
            .HasOne(sg => sg.Group)
            .WithMany(g => g.SensorGroups)
            .HasForeignKey(sg => sg.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EndUserGroup>()
            .HasOne(eug => eug.Group)
            .WithMany(g => g.EndUserGroups)
            .HasForeignKey(eug => eug.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}