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
        public VoteType VoteType { get; private set; }

        [JsonConstructor]
        public Rating(int userId, VoteType voteType)
        {
            UserId = userId;
            VoteType = voteType;
            Validate(userId, voteType);
        }

        public Rating() { }

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

        public void UpdateVote(VoteType newVoteType)
        {
            if (VoteType == newVoteType)
            {
                return;
            }
            VoteType = newVoteType;
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
