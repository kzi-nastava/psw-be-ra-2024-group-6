using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain.ShoppingCarts;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository
    {
        public ShoppingCart Create(ShoppingCart cart);
        public ShoppingCart Update(ShoppingCart cart);
        public void Delete(long id);
        public ShoppingCart Get(long id);
    }
}
