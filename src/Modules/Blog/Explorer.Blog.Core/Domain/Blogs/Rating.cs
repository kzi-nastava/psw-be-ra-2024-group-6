using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.Blogs
{
    public class Rating : ValueObject
    {
        public int UserId { get; }
        public VoteType VoteType { get; }

        [JsonConstructor]
        public Rating(int userId, VoteType voteType)
        {
            //Validate(userId, voteType);
            UserId = userId;
            VoteType = voteType;
        }

        public Rating()
        {

        }

        private void Validate(int userId, VoteType voteType)
        {
            if (userId == 0)
            {
                throw new ArgumentException("User ID must not be a zero.");
            }
            if (voteType == null)
            {
                throw new ArgumentException("Vote type cannot be null.");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] { VoteType, UserId };
        }


    }

    public enum VoteType
    {
        Upvote,
        Downvote
    }
}
