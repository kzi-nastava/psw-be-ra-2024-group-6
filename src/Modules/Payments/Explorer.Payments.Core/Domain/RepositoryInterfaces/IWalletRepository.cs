using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IWalletRepository
    {
        public Wallet Create(Wallet wallet);
        public Wallet Update(Wallet wallet);
        public Wallet? GetByUserId(long userId);
        public Wallet UpdatePrice(Wallet wallet, Price price);
    }
}
