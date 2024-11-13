using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos;

public class TourCardDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
//  public string ImageUrl { get; set; }
     public double AvarageRating { get; set; }
 //   public int ReviewsCount { get; set; }

    //    public string ImageUrl { get; set; }
    // public double Rating { get; set; }
    //   public int ReviewsCount { get; set; }

    public string Distance { get; set; }
    //   public string Country { get; set; }
    //   public string City { get; set; }

    public TourCardDto(long id, string name, double price, string distance,double avarageRating)
    {
        Id = id;
        Name = name;
        Price = price;
        Distance = distance;
        AvarageRating = avarageRating;
    }
}
