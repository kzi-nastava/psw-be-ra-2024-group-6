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

                var existingWallet = _dbContext.Wallets.Find(wallet.Id);
                if (existingWallet != null)
                {
                    _dbContext.Entry(existingWallet).State = EntityState.Detached;
                }
                _dbContext.Entry(wallet).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return wallet;
        }
        public Wallet UpdatePrice(Wallet wallet, Price price)
        {
            try
            {
                var existingWallet = _dbContext.Wallets.Find(wallet.Id);

                if (existingWallet == null)
                {
                    throw new KeyNotFoundException("Wallet not found.");
                }

                existingWallet.AdventureCoins = (long)price.Amount;

                _dbContext.Entry(existingWallet).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new ApplicationException("An error occurred while updating the wallet.", e);
            }

            return wallet;
        }
    }
}
