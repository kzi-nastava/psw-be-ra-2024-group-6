using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TouristFavorites : Entity
    {
        public int TouristId { get; set; }
        public List<int> FavoriteCheckpointIds { get; set; }

        public TouristFavorites()
        {
            FavoriteCheckpointIds = new List<int>();
        }

        public void AddToFavorites(int checkpointId)
        {
            FavoriteCheckpointIds.Add(checkpointId);
        }

        public void RemoveFromFavorites(int checkpointId)
        {
            FavoriteCheckpointIds.Remove(checkpointId);
        }
    }
}
