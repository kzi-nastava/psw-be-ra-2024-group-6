using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Shopping
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> Get(int id);
        Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        Result Delete(int id);

        public Result<ShoppingCartDto> GetByUserId(long userId);

        public Result<ShoppingCartDto> AddItem(long shoppingCartId, long tourId,long userId);
        public Result<ShoppingCartDto> RemoveItem(int shoppingCartId, int itemId);
    }
}
