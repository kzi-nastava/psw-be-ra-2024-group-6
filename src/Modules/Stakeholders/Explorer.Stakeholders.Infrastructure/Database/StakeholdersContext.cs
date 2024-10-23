using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public DbSet<Tourist> Tourists { get; set; }
	public DbSet<Problem> Problems { get; set; }
    public DbSet<Club> Clubs { get; set; }

    public DbSet<Author> Author { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);


        modelBuilder.Entity<Author>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Author>(a => a.UserId);

        modelBuilder.Entity<Tourist>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Tourist>(t => t.UserId );
		modelBuilder.Entity<Problem>()
            .HasOne<Tourist>()
            .WithMany()
            .HasForeignKey(p => p.TouristId);
        modelBuilder.Entity<Rating>()
            .HasOne<Rating>()
            .WithMany()
            .HasForeignKey(p => p.UserId);
        modelBuilder.Entity<Club>()
            .HasOne<User>() 
            .WithMany() 
            .HasForeignKey(c => c.OwnerId) 
            ;

    }


}