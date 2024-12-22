using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;

public class ObjectDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ImageData { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public LocationReadDto Location { get; set; }
    public long TourId { get; set; }
}
