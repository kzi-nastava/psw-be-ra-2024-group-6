using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Object = Explorer.Tours.Core.Domain.Tours.Object;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.API.Dtos.TourDtos;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Object> Objects { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }
    public DbSet<Tour> Tours { get; set; }

    public DbSet<TouristEquipmentManager> TouristEquipmentManagers { get; set; }
    public DbSet<TourExecution> TourExecutions { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Bundle> Bundles { get; set; }

    public DbSet<PublicCheckpointRequest> PublicCheckpointRequests { get; set; }
    public DbSet<RoadTrip> RoadTrips { get; set; }
    public DbSet<TouristFavorites> TouristFavorites { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        ConfigureTour(modelBuilder);
        ConfigureCheckpoint(modelBuilder);
        ConfigureObject(modelBuilder);
        ConfigureReview(modelBuilder);
        ConfigureEquipment(modelBuilder);
        ConfigureTouristEquipmentManager(modelBuilder);
        ConfigureBundle(modelBuilder);
        ConfigurePublicCheckpointRequest(modelBuilder);
        ConfigureRoadTrip(modelBuilder);
        ConfigureTouristFavorites(modelBuilder);

        modelBuilder.Entity<TourExecution>().Property(item => item.Position).HasColumnType("jsonb");
        modelBuilder.Entity<TourExecution>().Property(item => item.CompletedCheckpoints).HasColumnType("jsonb");
    }


    private void ConfigureBundle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bundle>(entity =>
        {
            entity.Property(e => e.Status)
               .HasConversion<string>();

        });
    }

    private void ConfigureEquipment(ModelBuilder modelBuilder)
    {
    }

    private void ConfigureObject(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Object>(entity =>
        {
            entity.HasOne<Tour>()
                .WithMany(t => t.Objects)
                .HasForeignKey(o => o.TourId);

            entity.Property(o => o.Location).HasColumnType("jsonb");
        });
    }

    private void ConfigureCheckpoint(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Checkpoint>(entity =>
        {
            entity.HasOne<Tour>()
                .WithMany(t => t.Checkpoints)
                .HasForeignKey(c => c.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(c => c.Location).HasColumnType("jsonb");


            entity.HasOne(c => c.PublicRequest)
                .WithOne()
                .HasForeignKey<PublicCheckpointRequest>(p => p.CheckpointId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }


    private void ConfigurePublicCheckpointRequest(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublicCheckpointRequest>(entity =>
        {
            entity.HasOne<Checkpoint>()
                .WithOne(c => c.PublicRequest)
                .HasForeignKey<PublicCheckpointRequest>(p => p.CheckpointId);

            entity.Property(p => p.Status)
                .HasConversion<string>();

            entity.Property(p => p.AdminComment)
                .HasMaxLength(500); 
        });
    }

    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasMany<Checkpoint>(t=>t.Checkpoints)
                  .WithOne()
                  .HasForeignKey(c => c.TourId);

            entity.HasMany<Object>(t =>t.Objects)
                  .WithOne()
                  .HasForeignKey(o => o.TourId);
            entity.HasMany(t => t.Equipment)
                  .WithMany()
                  .UsingEntity(j => j.ToTable("RequiredEquipments"));
            entity.HasMany(t =>t.Reviews).WithOne().HasForeignKey(o => o.TourId);
            entity.Property(tour => tour.Durations).HasColumnType("jsonb");
            entity.Property(tour => tour.Price).HasColumnType("jsonb");
            entity.Property(tour => tour.TotalLength).HasColumnType("jsonb");
        });
    }

    private static void ConfigureTouristEquipmentManager(ModelBuilder modelBuilder)
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
    }


    private static void ConfigureReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne<Tour>() 
                  .WithMany(t => t.Reviews)
                  .HasForeignKey(r => r.TourId);
        });
    }

    private void ConfigureRoadTrip(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoadTrip>(entity =>
        {
            entity.HasKey(roadTrip => roadTrip.Id);

            entity.Property(roadTrip => roadTrip.TotalLength)
                .HasColumnType("jsonb");

            entity.Property(roadTrip => roadTrip.PublicCheckpoints)
                .HasColumnType("jsonb");

            entity.Property(roadTrip => roadTrip.Tours)
                .HasColumnType("jsonb");
        });
    }

    private void ConfigureTouristFavorites(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TouristFavorites>(entity =>
        {
            entity.HasKey(tf => tf.Id);
            entity.Property(tf => tf.FavoriteCheckpointIds)
                .HasColumnType("jsonb");
        });
    }
}