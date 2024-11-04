using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;

public class TourPreviewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Difficulty { get; set; }
    public List<string> Tags { get; set; }
    public double Price { get; set; }
    public string AuthorName { get; set; }
    public string TotalLenght { get; set; }
    public List<string> Durations { get; set; }
    public CheckpointReadDto FirstCheckpoint { get; set; }

    public TourPreviewDto(long id, string name, string description, string difficulty, List<string> tags, double price, string authorName, string totalLenght, List<string> durations, CheckpointReadDto firstCheckpoint)
    {
        Id = id;
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Tags = tags;
        Price = price;
        AuthorName = authorName;
        TotalLenght = totalLenght;
        Durations = durations;
        FirstCheckpoint = firstCheckpoint;
    }

}
