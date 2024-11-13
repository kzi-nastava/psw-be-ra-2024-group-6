using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class RatingDto
    {
        public int UserId {  get; set; }
        public string VoteType {  get; set; }

        public RatingDto() { }
        public RatingDto(int userId, string voteType) {
            UserId = userId;
            VoteType = voteType;
        }
    }
}
