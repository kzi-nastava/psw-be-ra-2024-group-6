using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourDtos
{
    public class TourAuthorCardDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double AvarageRating { get; set; }
        public string Status { get; set; }
        public DateTime StatusChangeTime { get; set; }

        public string Distance { get; set; }


        public TourAuthorCardDto(long id, string name, double price, string distance, double avarageRating,string status,DateTime statusChangeTime)
        {
            Id = id;
            StatusChangeTime= statusChangeTime;
            Name = name;
            Price = price;
            Distance = distance;
            Status = status;
            AvarageRating = avarageRating;
        }
    }
}
