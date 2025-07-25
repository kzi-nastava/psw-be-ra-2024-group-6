using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class BundleDatabaseRepository : CrudDatabaseRepository<Bundle,ToursContext>, IBundleRepository
    {
        private readonly ToursContext _context;

        public BundleDatabaseRepository(ToursContext context) : base(context)
        {
            _context = context;
        }

        public List<Bundle> GetAll()
        {
            try
            {
                var ret = _context.Bundles.ToList();
                return ret;
            }
            catch(Exception ex) 
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public Bundle Create(Bundle bundle)
        {
            var bd = _context.Bundles.Add(bundle).Entity;
            _context.SaveChanges();
            return bd;
        }


        public Bundle GetById(long bundleId)
        {
            try
            {
                var ret = _context.Bundles.Find(bundleId);
                return ret;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public List<Bundle> GetByTourId(long tourId)
        {
            return _context.Bundles.Where(b => b.TourIds.Contains((int)tourId)).ToList();
        }

    }
}
