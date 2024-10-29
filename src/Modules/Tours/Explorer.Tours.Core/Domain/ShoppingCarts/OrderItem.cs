using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class OrderItem : Entity
    {
        public int ShoppingCartId { get; init; }
        public int TourId { get; init; }
        public string Name { get; init; }
        public Price Price { get; init; }


        public OrderItem(int shoppingCartId, int tourId, string name, Price price)
        {
            ShoppingCartId = shoppingCartId;
            TourId = tourId;
            Name = name;
            Price = price;
        }

    }
}
