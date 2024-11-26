using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain;

public class PurchaseToken : Entity
{
    public long UserId { get; init; }
    public long TourId { get; init; }
    public DateTime PurchaseDate { get; init; }
    public bool isExpired { get; init; }

    public PurchaseToken(long userId, long tourId, DateTime purchaseDate, bool isExpired)
    {
        UserId = userId;
        TourId = tourId;
        PurchaseDate = purchaseDate;
        this.isExpired = isExpired;
    }
}
