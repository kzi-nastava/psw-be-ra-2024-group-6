using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain;

public class OrderItem : Entity
{
    public long ShoppingCartId { get; init; }
    public long TourId { get; init; }
    public string Name { get; init; }
    public Price Price { get; init; }


    public OrderItem(long shoppingCartId, long tourId, string name, Price price)
    {
        ShoppingCartId = shoppingCartId;
        TourId = tourId;
        Name = name;
        Price = price;
    }
}
