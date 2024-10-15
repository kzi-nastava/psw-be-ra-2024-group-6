using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public enum Difficulty
    {
        Easy, Medium, Hard
    }
    public enum TransportOptions
    {
        Walk, Bicycle, Car, Boat
    }
    public class TourPreferenceDto
    {
        public int TourId { get; set; }
        public Difficulty Difficulty { get; set; }
        public Dictionary<TransportOptions, int> TransportOptionsScore { get; set; }
        public List<string> Tags { get; set; }
    }
}
