using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
public class CheckpointDto
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public int LocationId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

}
