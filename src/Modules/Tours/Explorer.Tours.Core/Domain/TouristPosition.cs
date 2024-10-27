using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TouristPosition
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public TouristPosition(double longitude, double latitude)
        {
            Validate(longitude, latitude);
            Longitude = longitude;
            Latitude = latitude;
        }

        public void Validate(double longitude, double latitude)
        {
            if (longitude is < -180 or > 180) throw new ArgumentException("Invalid longitude");
            if (latitude is < -90 or > 90) throw new ArgumentException("Invalid latitude");
        }
    }
}
