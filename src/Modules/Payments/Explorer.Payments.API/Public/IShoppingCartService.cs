using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<ShoppingCartDto> Get(int id);
    Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
    Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);

    Result<CouponDto>? CheckAndApplyCoupon(string code,int userId);
    Result Delete(int id);

    public Result<ShoppingCartDto> GetByUserId(long userId);

    public Result<ShoppingCartDto> AddItem(long userId, long resourceId, long resourceTypeId);
    public Result<ShoppingCartDto> RemoveItem(int userId, int resourceId);

    public Result<CheckoutResultDto> Checkout(int userId);
}
