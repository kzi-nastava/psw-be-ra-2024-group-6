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

    public class TourPreference : Entity
    {
        public int TourId { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public List<TransportOptionScore> TransportOptionScores { get; private set; }
        public List<string> Tags { get; private set; }

        public TourPreference(int tourId, Difficulty difficulty, List<string> tags)
        {
            TourId = tourId;
            Difficulty = difficulty;
            TransportOptionScores = new List<TransportOptionScore>();
            //TransportOptionScores = new List<TransportOptionScore>(transportOptionScores);
            Tags = new List<string>(tags);

            Validate();
        }

        private void Validate()
        {
            if (Tags == null || Tags.Count == 0)
            {
                throw new ArgumentException("Tags can not be null or empty.");
            }

            
            foreach (var transportOptionScore in TransportOptionScores)
            {
                if (transportOptionScore.Score < 0 || transportOptionScore.Score > 3)
                {
                    throw new ArgumentException("Score for transport options must be between 0 and 3.");
                }
            }
            
        }

    }
}
