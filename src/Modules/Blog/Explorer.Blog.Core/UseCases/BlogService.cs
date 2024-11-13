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
    public class BlogService : CrudService<BlogDto, BlogDomain.Blogs.Blog> ,IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IInternalInstructorService _internalInstructorService;
        private readonly IMapper _mapper;
        public BlogService(IBlogRepository blogRepository,ICrudRepository<BlogDomain.Blogs.Blog> repository, IMapper mapper, IInternalInstructorService internalInstructorService) : base(repository, mapper) {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _internalInstructorService = internalInstructorService;
        }

        public Result<BlogDto> GetBlogDetails(long id)
        {
            var blog = _blogRepository.Get(id);
            var blogDto = _mapper.Map<BlogDto>(blog);
            var result = _internalInstructorService.Get(blogDto.UserId ?? -1);
            if (result.IsSuccess)
            {
                UserDto user = result.Value;
                blogDto.Username = user.Username;
            }
            else
            {
                throw new Exception("User retrieval failed: " + string.Join(", ", result.Errors));
            }
            return blogDto;
        }

        public IEnumerable<BlogDto> GetAllBlogs()
        {
            
                var blogs = _blogRepository.GetAllBlogsWithPictures();
                return _mapper.Map<IEnumerable<BlogDto>>(blogs);
            
        }

    }
}
