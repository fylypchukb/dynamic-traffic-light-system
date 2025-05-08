using DynamicTrafficLightServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace DynamicTrafficLightServer.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Intersection> Intersections => Set<Intersection>();
    public DbSet<TrafficLight> TrafficLights => Set<TrafficLight>();
    public DbSet<Configuration> Configurations => Set<Configuration>();
    public DbSet<EntityChangeLog> EntityChangeLogs => Set<EntityChangeLog>();
    public DbSet<TrafficSwitchLog> TrafficSwitchLogs => Set<TrafficSwitchLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.AuthIdentityId)
            .IsUnique();

        modelBuilder.Entity<Intersection>()
            .HasOne(i => i.CreatedBy)
            .WithMany(u => u.CreatedIntersections)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Intersection>()
            .HasOne(i => i.LastUpdatedBy)
            .WithMany(u => u.LastUpdatedIntersections)
            .HasForeignKey(i => i.LastUpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TrafficLight>()
            .HasOne(t => t.CreatedBy)
            .WithMany(u => u.CreatedTrafficLights)
            .HasForeignKey(t => t.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TrafficLight>()
            .HasOne(t => t.LastUpdatedBy)
            .WithMany(u => u.LastUpdatedTrafficLights)
            .HasForeignKey(t => t.LastUpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Configuration>()
            .HasOne(c => c.CreatedBy)
            .WithMany(u => u.CreatedConfigurations)
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Configuration>()
            .HasOne(c => c.LastUpdatedBy)
            .WithMany(u => u.LastUpdateConfigurations)
            .HasForeignKey(c => c.LastUpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Configuration>()
            .Property(p => p.SequenceGreenTime)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v))
            .Metadata.SetValueComparer(new ValueComparer<Dictionary<int, int>>(false));

        modelBuilder.Entity<EntityChangeLog>()
            .HasOne(t => t.ChangedBy)
            .WithMany(u => u.EntityChangeLogs)
            .HasForeignKey(k => k.ChangedById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TrafficSwitchLog>()
            .HasOne(l => l.InitBy)
            .WithMany(u => u.TrafficSwitchLogs)
            .HasForeignKey(k => k.InitById)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TrafficSwitchLog>()
            .HasOne(l => l.TrafficLight)
            .WithMany(t => t.TrafficSwitchLogs)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                Name = "System",
                AuthIdentityId = "system"
            });
    }
}