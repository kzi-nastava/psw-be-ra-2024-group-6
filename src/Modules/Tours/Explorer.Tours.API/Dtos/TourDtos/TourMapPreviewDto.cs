using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.DurationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourMapPreviewDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public double Price { get; set; }
        public double AverageRating { get; set; }
        public string TourImage { get; set; }
        public int ReviewsCount { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorImage { get; set; }
        public long AuthorId { get; set; }
        public List<TourDurationDto> Durations { get; set; }
        public DistanceDto Distance { get; set; }
        public TourMapPreviewDto(long id,long authorId, string name,string authorSurname, string difficulty, double price, double averageRating, string tourImage, int reviewsCount, string authorName, string authorImage, List<TourDurationDto> durations, DistanceDto distance)
        {
            Id = id;
            Name = name;
            Difficulty = difficulty;
            Price = price;
            AverageRating = averageRating;
            TourImage = tourImage;
            ReviewsCount = reviewsCount;
            AuthorName = authorName;
            AuthorImage = authorImage;
            Durations = durations;
            Distance = distance;
            AuthorSurname = authorSurname;
            AuthorId = authorId;
        }



    }
}
