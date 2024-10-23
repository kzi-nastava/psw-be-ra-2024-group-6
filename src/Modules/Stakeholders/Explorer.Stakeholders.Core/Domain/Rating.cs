using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
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
        public int UserId { get; private set; }
        public StarRating StarRating { get; private set; }
        public string Comment { get; set; }
        public DateTime PostedAt { get; set; }

        public Rating(int userId, StarRating starRating, string comment) 
        {
            UserId = userId;
            StarRating = starRating;
            Comment = comment;
            PostedAt = DateTime.Now;
            Validate();
        }

        private void Validate()
        {
            if (UserId <= 0)
                throw new ArgumentException("UserId must be greater than 0.");

            // Check if the StarRating is valid
            if (!Enum.IsDefined(typeof(StarRating), StarRating))
                throw new ArgumentException("Star Rating must be a valid value between 1 and 5.");
        }
    }
}

