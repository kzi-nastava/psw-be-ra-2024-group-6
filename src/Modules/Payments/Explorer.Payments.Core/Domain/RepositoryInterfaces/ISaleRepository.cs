using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ISaleRepository
    {
        public ShoppingCart Create(ShoppingCart cart);
        public ShoppingCart Update(ShoppingCart cart);
        public void Delete(long id);
        public ShoppingCart? Get(long id);

        public ShoppingCart? GetByUserId(long id);
    }
}
