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
    public string Name { get; init; }
    public Price Price { get; init; }
    public long ProductId { get; init; }
    public Product Product { get; init; }

    public OrderItem() { }
    public OrderItem(long shoppingCartId, string name, Price price, Product product)
    {
        ShoppingCartId = shoppingCartId;
        Name = name;
        Price = price;
        Product = product;
        ProductId = product.Id;
    }
}
