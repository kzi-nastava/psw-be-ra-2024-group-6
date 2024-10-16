using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public enum TransportOptions
    {
        Walk, Bicycle, Car, Boat
    }
    public class TransportOptionScore : Entity
    {
        public long TourPreferenceId { get; set; } 
        public TransportOptions TransportOption { get; set; }
        public int Score { get; set; }
        
        public TransportOptionScore(long tourPreferenceId, TransportOptions transportOption, int score)
        {
            TourPreferenceId = tourPreferenceId;
            TransportOption = transportOption;
            Score = score;
        }

        public TransportOptionScore(int transportOption, int score)
        {
            TransportOption = (TransportOptions)transportOption;
            Score = score;
        }
    }
}
