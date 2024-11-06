using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain.Problems
{
    public class Problem : Entity
    {
        public string Category { get; private set; }
        public string Priority { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public long TourId { get; private set; }
        public long TouristId { get; private set; }
        public bool IsResolved { get; private set; }
        public bool IsClosed { get; private set; }
        public DateTime DueDate { get; private set; }
        public List<ProblemMessage> Messages { get; private set; }

        public Problem(string category, string priority, DateTime date, string description, long tourId, long touristId, DateTime? dueDate = null)
        {
            Category = category;
            Priority = priority;
            Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            Description = description;
            TourId = tourId;
            TouristId = touristId;
            IsClosed = false;
            IsResolved = false;
            Messages = new List<ProblemMessage>();
            DueDate = dueDate ?? DateTime.UtcNow.AddDays(5);
            Validate();
        }

        public Problem() {}

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Category)) throw new ArgumentException("Invalid Category.");
            if (string.IsNullOrWhiteSpace(Priority)) throw new ArgumentException("Invalid Priority");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        }

        public void AddMessage(ProblemMessage message)
        {

        }

        public void MarkAsResolved()
        {
            IsResolved = true;
        }

        public void Close()
        {
            IsClosed = true;
        }


    }
}