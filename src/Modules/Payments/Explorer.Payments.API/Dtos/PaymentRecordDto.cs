using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class PaymentRecordDto
    {
        public int Id { get; set; }
        public long TouristId { get; set; }
        public long ResourceId { get; set; }
        public int ResourceTypeId { get; set; }
        public double Price { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
