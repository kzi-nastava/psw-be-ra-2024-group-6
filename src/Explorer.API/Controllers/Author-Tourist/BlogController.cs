using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.UseCases;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author_Tourist
{
    [Authorize(Policy = "touristOrAuthorPolicy")]
    [Route("api/touristOrAuthor/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public ActionResult<BlogDto> Create([FromBody] BlogDto blog)
        {
            blog.UserId = User.UserId();
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BlogDto> Update([FromBody] BlogDto blog)
        {
            var result = _blogService.Update(blog);
            return CreateResponse(result);
        }

		[HttpGet("{id:int}")]
		public ActionResult<BlogDto> Get([FromQuery] int id)
        {
			return CreateResponse(_blogService.Get(id));
		}

		[HttpGet]
		public ActionResult<PagedResult<BlogDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = _blogService.GetPaged(page, pageSize);
			return CreateResponse(result);
		}

        [HttpDelete("{id:int}")]
		public ActionResult<BlogDto> Delete([FromQuery] int id)
		{
			return CreateResponse(_blogService.Delete(id));
		}

        [HttpGet("all")]
        public ActionResult<IEnumerable<BlogDto>> GetAllBlogs()
        {
            var blogs = _blogService.GetAllBlogs();
            return CreateResponse(blogs);
        }

        [HttpGet("blogDetails/{id:long}")]
        public ActionResult<BlogDetailsDto> GetBlogDetails([FromRoute] long id)
        {
            var blogResult = _blogService.GetBlogDetails(id);
            return CreateResponse(blogResult);
        }

        [HttpGet("byTag")]
        public ActionResult<List<BlogDto>> GetBlogsByTag([FromQuery] string tag)
        {
            var result = _blogService.GetBlogsByTag(tag);
            return CreateResponse(result);
        }
    }
}
