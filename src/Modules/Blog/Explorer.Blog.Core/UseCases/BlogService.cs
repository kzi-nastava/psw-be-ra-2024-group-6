using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDomain = Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogDto, BlogDomain.Blogs.Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IInternalInstructorService _internalInstructorService;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository,ICrudRepository<BlogDomain.Blogs.Blog> repository, IMapper mapper, IInternalInstructorService internalInstructorService) : base(repository, mapper) {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _internalInstructorService = internalInstructorService;
        }

        public Result<BlogDetailsDto> GetBlogDetails(long id)
        {
            var blog = _blogRepository.Get(id);
            UserDto blogAutor = _internalInstructorService.Get(blog.UserId).Value;
            var blogDto = _mapper.Map<BlogDetailsDto>(blog);
            blogDto.AuthorUsername = blogAutor.Username;

            foreach(CommentDto comment in blogDto.Comments)
            {
                UserDto commentAuthor = _internalInstructorService.Get(blog.UserId).Value;
                comment.AuthorUsername = commentAuthor.Username;

            }
            return blogDto;
        }

        public Result<List<BlogDto>> GetAllBlogs()
        {     
            var blogs = _blogRepository.GetAllBlogsWithPictures().ToList();
            return _mapper.Map<List<BlogDto>>(blogs);
        }

        public Result<List<BlogHomeDto>> GetHomePaged(int page, int pageSize)
        {
            List<Domain.Blogs.Blog> blogs = _blogRepository.GetAggregatePaged(page, pageSize);
            List<BlogHomeDto> blogDtos = new List<BlogHomeDto>();

            foreach (var blog in blogs)
            {
                blogDtos.Add(new BlogHomeDto()
                {
                    Description = blog.Description,
                    Id = blog.Id,
                    ImageUrl = blog.Pictures.FirstOrDefault()?.Data,
                    Title = blog.Title,
                    CreatedAt = blog.CreatedAt
                });
            }
            return blogDtos;
        }

        public Result<List<BlogHomeDto>> GetBlogsByTag(string tag)
        {
            var blogs = _blogRepository.GetBlogsByTag(tag).ToList();
            List<BlogHomeDto> blogDtos = new List<BlogHomeDto>();
            foreach (var blog in blogs)
            {
                blogDtos.Add(new BlogHomeDto()
                {
                    Description = blog.Description,
                    Id = blog.Id,
                    ImageUrl = blog.Pictures.FirstOrDefault()?.Data,
                    Title = blog.Title,
                    CreatedAt = blog.CreatedAt,
                    Tags = blog.Tags
                });
            }
            return blogDtos;
        }
    }
}
