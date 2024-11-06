using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain;

public class Review : Entity
{
    public long TouristId { get; private set; }
    public long TourId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public DateTime TourDate { get; private set; }
    public DateTime ReviewDate { get; private set; }
    public List<string> Images { get; private set; }

    public Tour Tour { get; private set; }


    public Review(long touristId, long tourId, int rating, string comment, DateTime tourDate, DateTime reviewDate, List<string> images)
    {
        TouristId = touristId;
        TourId = tourId;
        Rating = rating;
        Comment = comment;
        TourDate = tourDate;
        ReviewDate = reviewDate;
        Images = images ?? new List<string>();
        Validate();
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Comment))
            throw new ArgumentException("Comment cannot be blank.");
        if (Rating < 1 || Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");
        if (TourDate > DateTime.Now)
            throw new ArgumentException("Tour date cannot be in the future..");
        if (ReviewDate < TourDate)
            throw new ArgumentException("Review date must be greater than or equal to the tour date.");
    }


}
