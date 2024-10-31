using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Object = Explorer.Tours.Core.Domain.Object;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.BuildingBlocks.Core.Domain;

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
    public DbSet<TourExecution> TourExecutions { get; set; }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<PurchaseToken> PurchaseTokens { get; set; }


    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        ConfigureTour(modelBuilder);

        modelBuilder.Entity<TourExecution>().Property(item => item.Position).HasColumnType("jsonb");
        modelBuilder.Entity<TourExecution>().Property(item => item.CompletedCheckpoints).HasColumnType("jsonb");
        ConfigureRequiredEquipment(modelBuilder);
        ConfigureOrderItem(modelBuilder);
        ConfigureShoppingCart(modelBuilder);
        ConfigurePurchaseToken(modelBuilder);
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


        modelBuilder.Entity<Checkpoint>()
            .HasOne<Tour>() // povezuje Checkpoint sa Tour
            .WithMany(t => t.Checkpoints) // pretpostavljam da Tour ima Checkpoints kolekciju
            .HasForeignKey(c => c.TourId);


        // Object entitet
        modelBuilder.Entity<Object>()
            .HasOne<Tour>() // povezuje Object sa Tour
            .WithMany(t => t.Objects) // pretpostavljam da Tour ima Objects kolekciju
            .HasForeignKey(o => o.TourId);






        modelBuilder.Entity<Checkpoint>()
          .HasOne<Location>(c=>c.Location)
          .WithOne()
          .HasForeignKey<Checkpoint>(c => c.LocationId);


    }

    private static void ConfigureOrderItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(item => item.Price).HasColumnType("jsonb");
            entity.HasOne<ShoppingCart>().WithMany(sc => sc.OrderItems)
                .HasForeignKey(oi => oi.ShoppingCartId);

            entity.HasOne<Tour>().WithMany().HasForeignKey(oi => oi.TourId);
        });
    }


    private static void ConfigureShoppingCart(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.Property(item => item.TotalPrice).HasColumnType("jsonb");
            entity.HasMany(sc => sc.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

        });

    }
    private static void ConfigurePurchaseToken(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseToken>(entity =>
        {
            entity.HasOne<Tour>()
                .WithMany()
                .HasForeignKey(pt => pt.TourId);
        });

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