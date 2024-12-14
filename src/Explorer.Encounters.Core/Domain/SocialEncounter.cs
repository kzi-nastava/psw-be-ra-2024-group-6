using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class SocialEncounter : Encounter
    {
        public int PeopleCount { get; private set; }
        public int Radius { get; private set; }

        public SocialEncounter() { }
        public SocialEncounter(string name, string description, Location loc, int xp, Status status, TypeEncounter type, int peopleCount, int radius, int creatorId, bool isRequired) : base(name, description, loc, xp, status, type,creatorId,isRequired)
        {
            PeopleCount = peopleCount;
            Radius = radius;
        }

        public bool IsUserInRadius(Location userLocation)
        {
            const double EarthRadius = 6371000; // Radius of the Earth in meters

            // Convert latitude and longitude from degrees to radians
            var dLat = (userLocation.Latitude - Location.Latitude) * Math.PI / 180.0;
            var dLon = (userLocation.Longitude - Location.Longitude) * Math.PI / 180.0;

            // Convert start and end latitudes to radians
            var lat1 = Location.Latitude * Math.PI / 180.0;
            var lat2 = userLocation.Latitude * Math.PI / 180.0;

            // Haversine formula
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = EarthRadius * c;

            return distance <= Radius;
        }
    }
}
