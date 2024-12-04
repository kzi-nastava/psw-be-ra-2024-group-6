using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class WalletDto
    {
        public int? Id { get; set; }
        public long UserId { get; set; }
        public long AdventureCoins { get; set; }
    }
}
