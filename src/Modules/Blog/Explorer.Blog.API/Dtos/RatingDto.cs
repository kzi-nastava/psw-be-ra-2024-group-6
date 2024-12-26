using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogRatingDto
    {
        public int UserId {  get; set; }
        public int BlogId { get; set; }
        public string VoteType {  get; set; }

        public BlogRatingDto() { }
        public BlogRatingDto(int userId, string voteType, int blogId) {
            UserId = userId;
            BlogId = blogId;
            VoteType = voteType;
        }
    }
}
