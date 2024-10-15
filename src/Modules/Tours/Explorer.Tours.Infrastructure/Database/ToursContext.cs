using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<RequiredEquipment> RequiredEquipments { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        ConfigureRequiredEquipment(modelBuilder);
    }

    private static void ConfigureRequiredEquipment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RequiredEquipment>(entity =>
        {
            entity.HasKey(re => re.Id);

            entity.HasOne<Tour>()
                .WithMany()
                .HasForeignKey(re => re.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Equipment>()
                .WithMany()
                .HasForeignKey(re => re.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(re => new { re.TourId, re.EquipmentId })
                .IsUnique();
        });
    }
}