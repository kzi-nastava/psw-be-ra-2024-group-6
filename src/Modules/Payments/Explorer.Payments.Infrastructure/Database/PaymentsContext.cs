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
    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        ConfigureOrderItem(modelBuilder);
        ConfigureShoppingCart(modelBuilder);
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
}
