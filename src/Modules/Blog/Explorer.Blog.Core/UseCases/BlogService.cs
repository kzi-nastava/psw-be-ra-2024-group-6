using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDomain = Explorer.Blog.Core.Domain;


namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogDto, BlogDomain.Blogs.Blog> ,IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public BlogService(IBlogRepository blogRepository,ICrudRepository<BlogDomain.Blogs.Blog> repository, IMapper mapper) : base(repository, mapper) {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public Result<BlogDto> GetBlogDetails(long id)
        {
            var blog = _blogRepository.Get(id);
            return _mapper.Map<BlogDto>(blog);
        }

        public Result<List<BlogHomeDto>> GetHomePaged(int page, int pageSize)
        {
            List<Domain.Blogs.Blog> blogs = _blogRepository.GetAggregatePaged(page, pageSize);
            List<BlogHomeDto> blogDtos = new List<BlogHomeDto>();
            foreach (Domain.Blogs.Blog blog in blogs)
            {
                blogDtos.Add(new BlogHomeDto()
                {
                    Description = blog.Description,
                    Id = blog.Id,
                    ImageUrl = blog.Pictures.First().Url,
                    Title = blog.Title,
                    CreatedAt = blog.CreatedAt
                });
            }

            return blogDtos;
        }
    }
}
