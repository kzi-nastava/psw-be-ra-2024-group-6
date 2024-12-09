using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class Location : ValueObject
    {
        public double Latitude { get; }

        public double Longitude { get; }

        [JsonConstructor]

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }

   


    
}
