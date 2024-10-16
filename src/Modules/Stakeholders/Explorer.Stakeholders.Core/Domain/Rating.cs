using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum StarRating
    {
        Poor = 1,
        Fair = 2,
        Good = 3,
        Great = 4,
        Excellent = 5
    }

    public class Rating : Entity
    {
        public int RatingId { get; private set; }
        public int UserId { get; private set; }
        public StarRating StarRating { get; private set; }
        public string Comment { get; set; }
        public DateTime PostedAt { get; set; }

        public Rating(int ratingId, int userId, StarRating starRating, string comment) 
        {
            RatingId = ratingId;
            UserId = userId;
            starRating = StarRating;
            Comment = comment;
            PostedAt = DateTime.Now;
        }
    }
}

