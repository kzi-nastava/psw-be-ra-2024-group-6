using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;

public class TourCardDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
// public double Rating { get; set; }
 //   public int ReviewsCount { get; set; }
    public double Distance { get; set; }
 //   public string Country { get; set; }
 //   public string City { get; set; }

    public TourCardDto(long id, string name, double price, string imageUrl, double distance)
    {
        Id = id;
        Name = name;
        Price = price;
        ImageUrl = imageUrl;
        Distance = distance;
    }
}
