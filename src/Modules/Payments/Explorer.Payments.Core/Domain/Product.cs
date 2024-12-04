using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain;

public class Product : Entity
{
    public Price Price { get; init; }
    public int ResourceTypeId { get; init; }
    public long ResourceId { get; init; }

    public Product() { }
    public Product(Price price, int resourceTypeId, long resourceId)
    {
        Price = price;
        ResourceTypeId = resourceTypeId;
        ResourceId = resourceId;
    }
}
