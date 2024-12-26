using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos.TourDtos;
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

    public OrderItem AddBundle(long bundleId,string name, double price)
    {

        if(BundleAlreadyInItems(bundleId))
        {
            return OrderItems.FirstOrDefault(t => t.ProductId == bundleId);
        }

        var newPrice = new Price(price);
        var newProduct = new Product(newPrice,2,bundleId);
        var newItem = new OrderItem(Id, name, newPrice, newProduct);
        OrderItems.Add(newItem);
        CalculateTotalPrice(price, true);
        return newItem;

    }

    private bool TourAlreadyInItems(long? tourId)
    {
        return OrderItems.Any(item => item.ProductId == tourId && item.Product.ResourceTypeId == 1);
    }

    private bool BundleAlreadyInItems(long? bundleId)
    {
        return OrderItems.Any(item => item.ProductId == bundleId && item.Product.ResourceTypeId == 2);

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

        double total = 0;
        foreach (var item in OrderItems)
        {
            var token = new PurchaseToken(this.UserId, item.ProductId, DateTime.UtcNow, false);

            tokens.Add(token);
            total += item.Price.Amount;
        }

        OrderItems.Clear();
        TotalPrice = new Price(total);

        return tokens;
    }

    public void setPriceToZero()
    {
        TotalPrice = new Price(0);
    }
}
