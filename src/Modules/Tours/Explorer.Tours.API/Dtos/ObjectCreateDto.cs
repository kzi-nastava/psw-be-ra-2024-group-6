namespace Explorer.Tours.API.Dtos
{
    public class ObjectCreateDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public long? TourId { get; set; }
        public LocationCreateDto Location { get; set; }
    }
}