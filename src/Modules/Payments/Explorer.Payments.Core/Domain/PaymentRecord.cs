using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain;

public class PaymentRecord : Entity
{
    public long TouristId { get; init; }
    public long ResourceId { get; init; }
    public int ResourceTypeId { get; init; }
    public Price Price { get; init; }
    public DateTime PaymentDate { get; init; }

    public PaymentRecord() { }
    public PaymentRecord(long touristId, long resourceId, int resourceTypeId, Price price, DateTime paymentDate)
    {
        TouristId = touristId;
        ResourceId = resourceId;
        ResourceTypeId = resourceTypeId;
        Price = price;
        PaymentDate = paymentDate;
    }
}
