using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourDatabaseRepository : ITourRepository
    {
        private readonly ToursContext _context;

        public TourDatabaseRepository(ToursContext context)
        {
            _context = context;
        }
        public List<Tour> GetByUserId(long userId)
        {
            try
            {
                var ret = _context.Tours.Where(t => t.AuthorId == userId).ToList();
                return ret;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }

        }

    }
}
