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
    public class BundleDatabaseRepository : IBundleRepository
    {
        private readonly ToursContext _context;

        public BundleDatabaseRepository(ToursContext context)
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

        public Bundle Update(Bundle bundle)
        {
            try
            {
                _context.Entry(bundle).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return bundle;
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

    }
}
