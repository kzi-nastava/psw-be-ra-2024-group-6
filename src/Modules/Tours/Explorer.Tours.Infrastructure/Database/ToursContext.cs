using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Checkpoint> Checkpoint { get; set; }
    public DbSet<Tour> Tour { get; set; }
    public DbSet<Location> Location { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        ConfigureTour(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Checkpoint>()
            .HasOne<Location>()
            .WithOne()
            .HasForeignKey<Checkpoint>(c => c.LocationId);
        modelBuilder.Entity<Checkpoint>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(c => c.TourId);
    }
}