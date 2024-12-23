using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Dtos.TourDtos.ObjectDtos
{
    public class ObjectCreateDto
    {
        public string Name { get; set; }
        public string ImageData { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public long? TourId { get; set; }
        public LocationCreateDto Location { get; set; }
    }
}