using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;

namespace Explorer.Tours.API.Dtos.TourDtos;

public class TourPreviewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Difficulty { get; set; }
    public List<string> Tags { get; set; }
    public double Price { get; set; }
    public string AuthorName { get; set; }
    public string TotalLength { get; set; }
    public List<string> Durations { get; set; }
    public CheckpointReadDto FirstCheckpoint { get; set; }

    public List<TourReviewDto> Reviews { get; set; }

    public TourPreviewDto(long id, string name, string description, string difficulty, List<string> tags, double price, string authorName, string totalLength, List<string> durations, CheckpointReadDto firstCheckpoint, List<TourReviewDto> reviews)
    {
        Id = id;
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Tags = tags;
        Price = price;
        AuthorName = authorName;
        TotalLength = totalLength;
        Durations = durations;
        FirstCheckpoint = firstCheckpoint;
        Reviews = reviews;
    }

}
