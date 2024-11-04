using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
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
        public Tour Create(Tour tour)
        {
            var tr = _context.Tours.Add(tour).Entity;
            _context.SaveChanges();
            return tr;
        }

        public Tour Update(Tour tour)
        {
            try
            {
                _context.Entry(tour).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return tour;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _context.Tours.Remove(entity);
            _context.SaveChanges();
        }


        public Tour Get(long id)
        {
            var tour = _context.Tours.Where(t => t.Id == id)
                .Include(t => t.Checkpoints!).Include(t=>t.Objects).Include(t=>t.Equipment).FirstOrDefault();
            if (tour == null)
            {
                throw new KeyNotFoundException($"ShoppingCart not found: {id}");
            }
            return tour;
        }

        public Tour Get(long id)
        {
            try
            {
                var ret = _context.Tours.FirstOrDefault(t => t.Id == id);
                return ret;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }
    }
}
