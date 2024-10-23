using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Object = Explorer.Tours.Core.Domain.Object;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Core.Domain.Object> Objects { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Tour> Tours { get; set; }

    public DbSet<RequiredEquipment> RequiredEquipments { get; set; }
    public DbSet<TouristEquipmentManager> TouristEquipmentManagers { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        ConfigureTour(modelBuilder);
    }
    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<TouristEquipmentManager>()
        .HasKey(te => te.Id);

        modelBuilder.Entity<TouristEquipmentManager>()
        .HasIndex(te => new { te.TouristId, te.EquipmentId })
        .IsUnique();

        modelBuilder.Entity<TouristEquipmentManager>()
                .HasOne<Equipment>()
                .WithMany()
                .HasForeignKey(t => t.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Object>()
            .HasOne<Location>(o=> o.Location)
            .WithOne()
            .HasForeignKey<Object>(c => c.LocationId);
        modelBuilder.Entity<Object>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(c => c.TourId);
        modelBuilder.Entity<Object>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(c => c.TourId);



        modelBuilder.Entity<Tour>()
            .HasMany<Checkpoint>()
            .WithOne()
            .HasForeignKey(c => c.TourId);

        modelBuilder.Entity<Tour>()
            .HasMany<Object>()
            .WithOne()
            .HasForeignKey(c => c.TourId);

      




        modelBuilder.Entity<Checkpoint>()
          .HasOne<Location>(c=>c.Location)
          .WithOne()
          .HasForeignKey<Checkpoint>(c => c.LocationId);
        modelBuilder.Entity<Checkpoint>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(c => c.TourId);

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