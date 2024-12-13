using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Wallet : Entity
    {
        public long UserId { get; init; }
        public long AdventureCoins {  get; set; }

        public Wallet(long UserId, long AdventureCoins)
        {
            this.UserId = UserId;
            this.AdventureCoins = AdventureCoins;
            Validate();
        }
        private void Validate()
        {
            if (AdventureCoins < 0)
                throw new ArgumentException("Advenutre coins cannot be lesser than 0.");
        }

  
    }
}
