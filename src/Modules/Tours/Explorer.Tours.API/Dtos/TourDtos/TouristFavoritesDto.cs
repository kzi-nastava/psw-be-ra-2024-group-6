using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TouristFavoritesDto
    {
        public long Id { get; set; }
        public int TouristId { get; set; }
        public List<CheckpointReadDto> FavoriteCheckpoints { get; set; }

        public TouristFavoritesDto() { }
        public TouristFavoritesDto(long id, int touristId, List<CheckpointReadDto> favoriteCheckpoints)
        {
            Id = id;
            TouristId = touristId;
            FavoriteCheckpoints = favoriteCheckpoints;
        }
    }
}
