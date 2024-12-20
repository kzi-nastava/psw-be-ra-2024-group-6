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
        public List<int> TourIds { get; private set; }
        public List<int> PublicCheckpointIds { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public Distance TotalLength { get; private set; }

        public RoadTrip()
        {
            TourIds = new List<int>();
            PublicCheckpointIds = new List<int>();
        }

        public RoadTrip(int touristId, List<int> tourIds, List<int> publicCheckpointIds)
        {
            TouristId = touristId;
            TourIds = tourIds ?? new List<int>();
            PublicCheckpointIds = publicCheckpointIds ?? new List<int>();
        }

        public void CalculateDifficulty(List<Tour> tours)
        {
            if (!tours.Any())
            {
                Difficulty = Difficulty.Easy;
                return;
            }

            var difficultyCounts = tours
                .GroupBy(t => t.Difficulty)
                .ToDictionary(g => g.Key, g => g.Count());

            var mostFrequentDifficulty = difficultyCounts
                .OrderByDescending(d => d.Value)
                .ThenBy(d => d.Key)
                .First().Key;

            Difficulty = mostFrequentDifficulty;
        }

        public void CalculateTotalLength(List<Tour> tours)
        {
            if (!tours.Any())
            {
                Difficulty = Difficulty.Easy;
                return;
            }

            var unit = tours.First().TotalLength.Unit;
            TotalLength = new Distance(tours.Sum(t => t.TotalLength.Length), unit);
        }
    }
}
