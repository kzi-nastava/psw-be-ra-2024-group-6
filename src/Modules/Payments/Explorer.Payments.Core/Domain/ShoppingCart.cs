using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain;

public class ShoppingCart : Entity
{
    public long UserId { get; private set; }
    public Price TotalPrice { get; private set; }
    public List<OrderItem> OrderItems { get; private set; }

    public ShoppingCart() { }
    public ShoppingCart(long userId, Price totalPrice)
    {
        UserId = userId;
        TotalPrice = totalPrice;
        OrderItems = new List<OrderItem>();
    }



    public OrderItem AddTour(long tourId, string name, double price)
    {
        if (TourAlreadyInItems(tourId))
        {
            return OrderItems.FirstOrDefault(t => t.ProductId == tourId);
        }

        var newPrice = new Price(price);
        var newProduct = new Product(newPrice, 1, tourId);
        var newItem = new OrderItem(Id, name, newPrice, newProduct);
        OrderItems.Add(newItem);
        CalculateTotalPrice(price, true);
        return newItem;
    }

    private bool TourAlreadyInItems(long? tourId)
    {
        return OrderItems.Any(item => item.ProductId == tourId);
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
            var token = new PurchaseToken(this.UserId, item.ProductId, DateTime.UtcNow, false);

            tokens.Add(token);
        }

        OrderItems.Clear();
        TotalPrice = new Price(0);

        return tokens;
    }
}
