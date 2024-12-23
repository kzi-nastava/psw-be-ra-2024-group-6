using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class RoadTripDatabaseRepository : IRoadTripRepository
    {
        private readonly ToursContext _context;
        public RoadTripDatabaseRepository(ToursContext dbContext)
        {
            _context = dbContext;
        }

        public RoadTrip Create(RoadTrip roadTrip)
        {
            _context.RoadTrips.Add(roadTrip);
            _context.SaveChanges();
            return roadTrip;
        }

        public List<RoadTrip> GetAllByTouristId(int touristId)
        {
            return _context.RoadTrips.Where(rt => rt.TouristId == touristId).ToList();
        }

        public RoadTrip Get(long id)
        {
            return _context.RoadTrips.Find(id);
        }
    }
}
