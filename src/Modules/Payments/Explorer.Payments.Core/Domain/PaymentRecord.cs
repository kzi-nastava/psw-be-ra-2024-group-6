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
    public long TourId { get; init; }
    public Price Price { get; init; }
    public DateTime PaymentDate { get; init; }

    public PaymentRecord() { }
    public PaymentRecord(long touristId, long tourId, Price price, DateTime paymentDate)
    {
        TouristId = touristId;
        TourId = tourId;
        Price = price;
        PaymentDate = paymentDate;
    }
}
