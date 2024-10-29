using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
        public long UserId { get; private set; }
        public Price TotalPrice { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public ShoppingCart(long userId, Price totalPrice, List<OrderItem> orderItems)
        {
            UserId = userId;
            TotalPrice = totalPrice;
            OrderItems = new List<OrderItem>();
        }

        public void AddItem() { }
        public void RemoveItem() { }

        public void CalculateTotalPrice()
        {
        }
    }
}
