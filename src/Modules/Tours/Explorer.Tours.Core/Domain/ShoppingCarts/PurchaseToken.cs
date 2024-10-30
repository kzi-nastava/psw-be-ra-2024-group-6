using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class PurchaseToken : Entity
    {
        public long UserId { get; init; }
        public long TourId { get; init; }
        public DateTime PurchaseDate { get; init; }

        public PurchaseToken(long userId, long tourId, DateTime purchaseDate)
        {
            UserId = userId;
            TourId = tourId;
            PurchaseDate = purchaseDate;
        }
    }
}
