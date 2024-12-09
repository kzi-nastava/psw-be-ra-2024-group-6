using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class HiddenEncounterExecution : EncounterExecution
    {
        public int TimePassedInSeconds { get; set; }

        public HiddenEncounterExecution(long encounterId, int touristId) : base(encounterId, touristId)
        {
            TimePassedInSeconds = 1;
        }

        public HiddenEncounterExecution() { }

        private bool AreLocationsWithinDistance(double touristLocaionLatitude, double touristLocationLongitude, double imageLocationLatitude, double imageLocationLongitude)
        {
            const double EarthRadiusMeters = 6371000;

            double toRadians = Math.PI / 180;
            double lat1Rad = touristLocaionLatitude * toRadians;
            double lon1Rad = touristLocationLongitude * toRadians;
            double lat2Rad = imageLocationLatitude * toRadians;
            double lon2Rad = imageLocationLongitude * toRadians;

            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusMeters * c;

            return distance <= 5;
        }

        // Za api poziv na svake 2 sekunde
        public void AddTimePassed(double touristLocaionLatitude, double touristLocationLongitude, double imageLocationLatitude, double imageLocationLongitude)
        {
            if(AreLocationsWithinDistance(touristLocaionLatitude, touristLocationLongitude, imageLocationLatitude, imageLocationLongitude))
                TimePassedInSeconds += 2;
            else
                TimePassedInSeconds = 1;

        }

        public bool Did30SecondsPass()
        {
            return TimePassedInSeconds > 30;
        }
    }
}
