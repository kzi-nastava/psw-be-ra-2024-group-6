using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
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
            var entity = GetAggregate(id);
            _context.Tours.Remove(entity);
            _context.SaveChanges();
        }


        public Tour GetAggregate(long id)
        {
            try
            {
                var ret = _context.Tours
                    .Include(t => t.Checkpoints)
                    .Include(t => t.Objects)
                    .Include(t => t.Equipment)
                    .Include(t => t.Reviews)
                    .FirstOrDefault(t => t.Id == id);
                return ret;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
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

        public Tour GetTourWithReviews(long tourId)
        {
            try
            {
                var tourWithReviews = _context.Tours
                    .Include(t => t.Reviews)  
                    .FirstOrDefault(t => t.Id == tourId);

                if (tourWithReviews == null)
                {
                    throw new KeyNotFoundException($"Tour with ID {tourId} not found.");
                }

                return tourWithReviews;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public PagedResult<Tour> GetToursWithReviews(int page,int size)
        {
            var result = _context.Tours.Include(t=>t.Reviews).GetPaged(page, size);
            result.Wait();
            return result.Result;


        }


    }
}
