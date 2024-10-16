using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourPreference> TourPreferences { get; set; }
    public DbSet<TransportOptionScore> TransportOptions { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        //ConfigureTourPreference(modelBuilder);
    }
    private static void ConfigureTourPreference(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransportOptionScore>().HasOne<TourPreference>().WithMany().HasForeignKey(tos => tos.TourPreferenceId);
    }

}