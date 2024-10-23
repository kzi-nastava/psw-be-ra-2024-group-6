using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckpointReadDto
    {
        
        public int Id { get; set; }
        public LocationReadDto Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public CheckpointReadDto(int id,LocationReadDto location,string name,string description,string imageUrl) {
            Id = id;   
            Location = location;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}
