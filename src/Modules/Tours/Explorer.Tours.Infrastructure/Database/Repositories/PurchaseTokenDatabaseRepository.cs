using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
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
    }
}
