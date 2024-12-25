using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TouristFavoritesDatabaseRepository : ITouristFavoritesRepository
    {
        private readonly ToursContext _context;

        public TouristFavoritesDatabaseRepository(ToursContext context)
        {
            _context = context;
        }

        public TouristFavorites GetByTouristId(int touristId)
        {
            return _context.TouristFavorites.FirstOrDefault(tpc => tpc.TouristId == touristId);
        }
    }
}
