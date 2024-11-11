using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Core.Domain.Problems;
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

    public DbSet<Notification> Notifications { get; set; }

    
    


    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<Problem>().Property(item => item.Messages).HasColumnType("jsonb");

        ConfigureStakeholder(modelBuilder);

        ConfigurePerson(modelBuilder);

        ConfigureNotification(modelBuilder); 
    }

    private void ConfigurePerson(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().Property(item => item.Followers).HasColumnType("jsonb");
        modelBuilder.Entity<Person>().Property(item => item.Followings).HasColumnType("jsonb");

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

        /*modelBuilder.Entity<Tourist>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Tourist>(t => t.UserId );*/
		modelBuilder.Entity<Problem>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.TouristId);
        modelBuilder.Entity<Club>()
            .HasOne<User>() 
            .WithMany() 
            .HasForeignKey(c => c.OwnerId);
    }

    private static void ConfigureNotification(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>()
            .HasKey(n => n.Id);

        modelBuilder.Entity<Notification>()
            .HasOne<Person>()
            .WithMany()
            .HasForeignKey(n => n.ReceiverPersonId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Notification>()
            .HasOne<Person>()
            .WithMany()
            .HasForeignKey(n => n.SenderPersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}