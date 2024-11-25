using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class CouponDTO
    {
        public int Id { get; set; }
        public string Code {get; set; }

        public double DiscountPercentage { get;set; }
        public long AuthorId { get; set; }
        public long? TourId { get; set; }
        public DateTime ExpiresDate { get; set; }
    }
}
