using Explorer.Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using BlogDomain = Explorer.Blog.Core.Domain;


namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<Comment> Comment { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.HasDefaultSchema("blog");
    }
}