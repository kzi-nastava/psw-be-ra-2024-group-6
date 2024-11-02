using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ILocationService
    {
        LocationDto Create(LocationCreateDto location);
    }
}
