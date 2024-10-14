using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Problem : Entity
    {
        public string Category { get; private set; }
        public string Priority { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public int TourId { get; private set; }
        public int TouristId { get; private set; }

        public Problem(string category, string priority, DateTime date,string description, int tourId, int touristId)
        {
            this.Category = category;
            this.Priority = priority;
            this.Date = date;
            this.Description = description;
            this.TourId = tourId;
            this.TouristId = touristId;
        }

    }
}
