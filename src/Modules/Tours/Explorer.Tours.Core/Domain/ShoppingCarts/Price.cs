using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class Price : ValueObject
    {
        public double Amount { get;  }

        [JsonConstructor]
        public Price(double amount)
        {
            Amount = amount;
        }

        public Price Add(Price price)
        {
            return new Price(price.Amount + Amount);
        }

        public Price Remove(Price price)
        {
            return new Price(Amount - price.Amount);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
