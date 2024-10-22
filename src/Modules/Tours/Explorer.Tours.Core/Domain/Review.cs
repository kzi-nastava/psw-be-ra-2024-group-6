using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Review : Entity
{
    public int TouristId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public DateTime TourDate { get; private set; }
    public DateTime ReviewDate { get; private set; }
    public List<string> Images { get; private set; }

    public Review(int touristId, int rating, string comment, DateTime tourDate, DateTime reviewDate, List<string> images)
    {
                  this.TouristId = touristId;
        Rating = rating;
        Comment = comment;
        TourDate = tourDate;
        ReviewDate = reviewDate;
        Images = images ?? new List<string>();
        Validate();
    }

    public void Validate()
    {
        //// Validacija parametara
        //if (rating < 1 || rating > 5)
        //    throw new ArgumentException("Rating mora biti između 1 i 5.");

        //if (string.IsNullOrWhiteSpace(comment))
        //    throw new ArgumentException("Komentar ne može biti prazan.");

        //if (tourDate > DateTime.Now)
        //    throw new ArgumentException("Datum ture ne može biti u budućnosti.");

        //if (reviewDate < tourDate)
        //    throw new ArgumentException("Datum recenzije ne može biti pre datuma ture.")
    }
}
