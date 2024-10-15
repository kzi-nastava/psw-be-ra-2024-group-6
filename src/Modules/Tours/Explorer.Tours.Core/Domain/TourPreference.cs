using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public enum Difficulty
    {
        Easy, Medium, Hard
    }
    public enum TransportOptions
    {
        Walk, Bicycle, Car, Boat
    }

    public class TourPreference : Entity
    {
        public int TourId { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public Dictionary<TransportOptions, int> TransportOptionsScore { get; private set; }
        public List<string> Tags { get; private set; }

        public TourPreference(int tourId, Difficulty difficulty, Dictionary<TransportOptions, int> transportOptionsScore, List<string> tags)
        {
            TourId = tourId;
            Difficulty = difficulty;
            TransportOptionsScore = new Dictionary<TransportOptions, int>(transportOptionsScore);
            Tags = new List<string>(tags);

            Validate();
        }

        private void Validate()
        {
            if (Tags == null || Tags.Count == 0)
            {
                throw new ArgumentException("Tags can not be null or empty.");
            }

            foreach (var score in TransportOptionsScore.Values)
            {
                if (score < 0 || score > 3)
                {
                    throw new ArgumentException("Score for transport options must be between 0 and 3.");
                }
            }
        }

    }
}
