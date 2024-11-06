using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.UseCases;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
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
		public ActionResult<PagedResult<CommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = _blogService.GetPaged(page, pageSize);
			return CreateResponse(result);
		}
		[HttpDelete("{id:int}")]
		public ActionResult<CommentDto> Delete([FromQuery] int id)
		{
			return CreateResponse(_blogService.Delete(id));
		}
	}
}
