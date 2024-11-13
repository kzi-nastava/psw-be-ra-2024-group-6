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

        public ShoppingCart(long userId, Price totalPrice)
        {
            UserId = userId;
            TotalPrice = totalPrice;
            OrderItems = new List<OrderItem>();
        }

        public OrderItem AddItem(long tourId, string name, double price)
        {
            if (TourAlreadyInItems(tourId))
            {
                return OrderItems.FirstOrDefault(t => t.TourId == tourId);
            }

            var newPrice = new Price(price);
            var newItem = new OrderItem(Id, tourId, name, newPrice);
            OrderItems.Add(newItem);
            CalculateTotalPrice(price, true);
            return newItem;
        }

        private bool TourAlreadyInItems(long tourId)
        {
            return OrderItems.Any(item => item.TourId == tourId);
        }

        public void RemoveItem(int itemId)
        {
            var itemToRemove = OrderItems.FirstOrDefault(t => t.Id == itemId);

            if (itemToRemove != null)
            {
                OrderItems.Remove(itemToRemove);
                CalculateTotalPrice(itemToRemove.Price.Amount, false);
            }

        }

        private void CalculateTotalPrice(double newPrice, bool add)
        {
            if (add)
            {
                TotalPrice = new Price(newPrice + TotalPrice.Amount);
            }
            else
            {
                TotalPrice = new Price(TotalPrice.Amount - newPrice);
            }
        }

        public List<PurchaseToken> Checkout()
        {
            var tokens = new List<PurchaseToken>();

            foreach (var item in OrderItems)
            {
                var token = new PurchaseToken(this.UserId, item.TourId, DateTime.UtcNow,false);

                tokens.Add(token);
            }

            OrderItems.Clear();
            TotalPrice = new Price(0);

            return tokens;
        }
    }
}