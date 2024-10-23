using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckpointCreateDto
    {

        public LocationCreateDto Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public CheckpointCreateDto(LocationCreateDto location, string name, string description, string imageUrl)
        {
            
            Location = location;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}
