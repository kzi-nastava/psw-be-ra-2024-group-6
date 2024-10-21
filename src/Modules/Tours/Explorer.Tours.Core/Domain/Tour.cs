

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public enum Status
    {
        Draft,
        Published,
        Closed
    }

    public class Tour : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set;}
        public Difficulty Difficulty { get; private set; }
        public List<string> Tags { get; private set; } // Lista tagova koji opisuju turu
        public double Cost { get; private set; } = 0; // Default cena je 0
        public Status Status { get; private set; } = Status.Draft; // Default status je Draft
        
        public long AuthorId { get; private set; }


        public Tour(string name, string description, Difficulty difficulty, List<string> tags,long authorId)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Tags = new List<string>(tags); // Kreira kopiju liste tagova
            AuthorId = authorId;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Description is required.");
            if (Tags == null || Tags.Count == 0) throw new ArgumentException("At least one tag is required.");

        }

    }
}
