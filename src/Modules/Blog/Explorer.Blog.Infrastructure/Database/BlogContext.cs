using Explorer.Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using BlogDomain = Explorer.Blog.Core.Domain;


namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<BlogDomain.Blog> Blogs { get; set; }
    public DbSet<BlogPicture> BlogPictures { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogDomain.Blog>()
                    .HasMany(b => b.Pictures) // Blog ima mnogo slika
                    .WithOne(p => p.Blog)      // Slika pripada jednom blogu
                    .HasForeignKey(p => p.BlogId)  // Strani ključ je BlogId
                    .OnDelete(DeleteBehavior.Cascade);  // Kada se obriše blog, brišu se i slike
        modelBuilder.HasDefaultSchema("blog");
    }
}