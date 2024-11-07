using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PurchaseTokenDatabaseRepository : IPurchaseTokenRepository
    {
        private readonly ToursContext _dbContext;

        public PurchaseTokenDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PurchaseToken Create(PurchaseToken token)
        {
            var pt = _dbContext.PurchaseTokens.Add(token).Entity;
            _dbContext.SaveChanges();
            return pt;
        }

        public List<PurchaseToken> GetByUserId(long id)
        {
            return _dbContext.PurchaseTokens
                .Where(t => t.UserId == id)
                .ToList();
        }

        public PurchaseToken? GetByUserAndTour(long userId, long tourId)
        {
            return _dbContext.PurchaseTokens
                .FirstOrDefault(t => t.UserId == userId && t.TourId == tourId);
        }

        public PurchaseToken Update(PurchaseToken token)
        {
            try
            {
                var existingToken = _dbContext.PurchaseTokens.Find(token.Id);
                if (existingToken != null)
                {
                    _dbContext.Entry(existingToken).State = EntityState.Detached;
                }

                _dbContext.Entry(token).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return token;
        }
    }
}
