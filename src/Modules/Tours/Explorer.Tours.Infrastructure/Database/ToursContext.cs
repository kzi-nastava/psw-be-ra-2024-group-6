using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Object = Explorer.Tours.Core.Domain.Object;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Core.Domain.Object> Objects { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Location> Locations { get; set; }
    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        ConfigureTour(modelBuilder);
    }
    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Object>()
            .HasOne<Location>()
            .WithOne()
            .HasForeignKey<Object>(c => c.LocationId);
        modelBuilder.Entity<Object>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(c => c.TourId);
    }
}