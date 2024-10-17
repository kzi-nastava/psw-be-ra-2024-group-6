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
        }

        /*private void Validate()
        {
            if (StarRating < 1 )
            {
                return BadRequest("Invalid star rating provided.");
            }
        }*/
    }
}

