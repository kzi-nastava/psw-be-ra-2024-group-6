using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class DestinationDto
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }

        public DestinationDto() {}

        public DestinationDto(string city, string country, string image)
        {
            City = city;
            Country = country;
            Image = image;
        }
    }
}
