using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class RoadTrip : Entity
    {
        public int TouristId { get; private set; }
        public List<Tour> Tours { get; private set; }
        public List<Checkpoint> PublicCheckpoints { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public Distance TotalLength { get; private set; }

        public RoadTrip()
        {
            Tours = new List<Tour>();
            PublicCheckpoints = new List<Checkpoint>();
        }

        public RoadTrip(int touristId, List<Tour> tours, List<Checkpoint> publicCheckpoints)
        {
            TouristId = touristId;
            Tours = tours ?? new List<Tour>();
            PublicCheckpoints = publicCheckpoints ?? new List<Checkpoint>();
        }

        public void CalculateDifficulty()
        {
            if (!Tours.Any())
            {
                Difficulty = Difficulty.Easy;
                return;
            }

            var difficultyCounts = Tours
                .GroupBy(t => t.Difficulty)
                .ToDictionary(g => g.Key, g => g.Count());

            var mostFrequentDifficulty = difficultyCounts
                .OrderByDescending(d => d.Value)
                .ThenBy(d => d.Key)
                .First().Key;

            Difficulty = mostFrequentDifficulty;
        }

        public void CalculateTotalLength()
        {
            if (!Tours.Any())
            {
                Difficulty = Difficulty.Easy;
                return;
            }

            var unit = Tours.First().TotalLength.Unit;
            TotalLength = new Distance(Tours.Sum(t => t.TotalLength.Length), unit);
        }
    }
}
