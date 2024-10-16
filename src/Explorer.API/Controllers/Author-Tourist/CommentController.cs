using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Blog.Core.Domain;

namespace Explorer.API.Controllers.Author_Tourist
{
    [Authorize(Policy = "touristOrAuthorPolicy")]
    [Route("api/touristOrAuthor/comment")]
    public class BlogController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly ICrudRepository<Comment> _commentRepository;

        public BlogController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult<CommentDto> Create([FromBody] CommentDto comment)
        {
            var result = _commentService.Create(comment);

            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CommentDto> Update([FromBody] CommentDto comment)
        {
            var result = _commentService.Update(comment);
            comment.CreationDate = _commentRepository.Get(comment.Id).CreationDate;
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CommentDto> Get([FromQuery] int id)
        {
            return CreateResponse(_commentService.Get(id));
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CommentDto> Delete([FromQuery] int id)
        {
            return CreateResponse(_commentService.Delete(id));
        }
    }
}
