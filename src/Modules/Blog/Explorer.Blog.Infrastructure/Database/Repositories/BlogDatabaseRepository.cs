using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDomain = Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogDatabaseRepository: IBlogRepository
    {
        private readonly BlogContext _dbContext;
        public BlogDatabaseRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BlogDomain.Blogs.Blog Create(BlogDomain.Blogs.Blog blog)
        {
            var sc = _dbContext.Blogs.Add(blog).Entity;
            _dbContext.SaveChanges();
            return sc;
        }
        public BlogDomain.Blogs.Blog Update(BlogDomain.Blogs.Blog blog)
        {
            try
            {
                _dbContext.Entry(blog).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return blog;
        }
        public void Delete(long id)
        {
            var entity = Get(id);
            _dbContext.Blogs.Remove(entity);
            _dbContext.SaveChanges();
        }
        public BlogDomain.Blogs.Blog Get(long id)
        {
            var blog = _dbContext.Blogs.Where(t => t.Id == id)
                .Include(t => t.Pictures).FirstOrDefault();
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog not found: {id}");
            }
            return blog;
        }

        public List<Core.Domain.Blogs.Blog>  GetAggregatePaged(int page, int pageSize)
        {
            return _dbContext.Blogs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(b=>b.Pictures)
                .ToList();
        }

    }
    
}
