using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Problem : Entity
    {
        public string Category { get; private set; }
        public string Priority { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public long TourId { get; private set; }
        public long TouristId { get; private set; }

        public Problem(string category, string priority, DateTime date, string description, long tourId, long touristId)
        {
            this.Category = category;
            this.Priority = priority;


            this.Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            this.Description = description;
            this.TourId = tourId;
            this.TouristId = touristId;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Category)) throw new ArgumentException("Invalid Category.");
            if (string.IsNullOrWhiteSpace(Priority)) throw new ArgumentException("Invalid Priority");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        }

    }
}