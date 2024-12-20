using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Utils
{
    public static class Utils
    {
        public static double CalculateHaversineDistance( double latitude, double longitude,double latitude2, double longitude2)
        {
            const double R = 6371; // Poluprečnik Zemlje u kilometrima
            double dLat = DegreesToRadians(latitude2 - latitude);
            double dLon = DegreesToRadians(longitude2 - longitude);

            // Haversine formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(latitude)) * Math.Cos(DegreesToRadians(latitude2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // Rezultat u kilometrima
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
