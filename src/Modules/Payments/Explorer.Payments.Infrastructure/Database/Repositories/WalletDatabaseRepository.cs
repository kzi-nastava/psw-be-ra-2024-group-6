using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class WalletDatabaseRepository : IWalletRepository
    {
        private readonly PaymentsContext _dbContext;

        public WalletDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Wallet Create(Wallet wallet)
        {
            var w = _dbContext.Wallets.Add(wallet).Entity;
            _dbContext.SaveChanges();
            return w;
        }

        public Wallet? GetByUserId(long userId)
        {
            var wallet = _dbContext.Wallets.Where(w => w.UserId == userId).FirstOrDefault();
            return wallet;
        }

        public Wallet Update(Wallet wallet)
        {
            try
            {
                _dbContext.Entry(wallet).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return wallet;
        }
    }
}
