using Explorer.Payments.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PurchaseToken> PurchaseTokens { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<PaymentRecord> PaymentRecords { get; set; }
    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        ConfigureOrderItem(modelBuilder);
        ConfigureShoppingCart(modelBuilder);
        ConfigureWallet(modelBuilder);
        ConfigureProduct(modelBuilder);
        ConfigureCoupon(modelBuilder);
        ConfigurePaymentRecord(modelBuilder);
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
    private static void ConfigureOrderItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(item => item.Price).HasColumnType("jsonb");
            entity.HasOne<ShoppingCart>().WithMany(sc => sc.OrderItems)
                .HasForeignKey(oi => oi.ShoppingCartId);
        });
    }
    private static void ConfigureWallet(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.UserId).IsRequired();
            entity.Property(w => w.AdventureCoins).IsRequired();
            //entity.HasIndex(w => w.UserId).IsUnique();
        });
    }
    private static void ConfigureProduct(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(product => product.Price).HasColumnType("jsonb");
            entity.HasOne<OrderItem>()
                  .WithOne(oi => oi.Product)
                  .HasForeignKey<OrderItem>(oi => oi.ProductId);
        });
    }

    private static void ConfigureCoupon(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>(entity =>
        {

            entity.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(8); 

            entity.HasIndex(c => c.Code)
                .IsUnique();


            entity.Property(c => c.ExpiresDate)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null, // Ako ima vrednost, konvertuj u UTC
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null // Ako ima vrednost, postavi na UTC
                );
        });
    }

    private static void ConfigurePaymentRecord(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaymentRecord>(entity =>
        {
            entity.Property(product => product.Price).HasColumnType("jsonb");
        });
    }
}
