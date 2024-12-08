using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourHoverMapDto
    {
        public long Id { get; set; }
        public LocationReadDto Location { get; set; }
        public double AverageRating { get; set; }
        public string TourName { get; set; }
        public double Price { get; set; }
        public string Difficulty { get; set; }
        public string Image { get; set; }
        public bool IsLocationUnique { get; set; }

        public TourHoverMapDto(LocationReadDto location, string difficulty, double averageRating, string tourName,
            double price, long id, string image, bool isLocationUnique)
        {
            Location = location;
            Difficulty = difficulty;
            AverageRating = averageRating;
            TourName = tourName;
            Price = price;
            Id = id;
            Image = image;
            IsLocationUnique = isLocationUnique;
        }
    }
}
