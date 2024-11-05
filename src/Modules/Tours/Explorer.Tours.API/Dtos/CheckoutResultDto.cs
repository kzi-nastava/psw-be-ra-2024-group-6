using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckoutResultDto
    {
        public ShoppingCartDto ShoppingCart { get; set; }
        public List<PurchaseTokenDto> PurchaseTokens { get; set; }
    }
}

