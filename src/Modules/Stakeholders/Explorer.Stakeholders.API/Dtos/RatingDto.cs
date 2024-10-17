using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public enum StarRating
    {
        Poor = 1,
        Fair = 2,
        Good = 3,
        Great = 4,
        Excellent = 5
    }

    public class RatingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public StarRating StarRating { get; set; }
        public string Comment { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
