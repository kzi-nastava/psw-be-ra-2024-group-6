using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Sale : Entity
    {

        public List<int> TourIds { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public double SalePercentage { get; private set; } 


        public Sale(List<int> tourIds, DateTime startDate, DateTime endDate, double salePercentage)
        {
            if (DateTime.Compare(endDate, startDate.AddDays(14)) < 0) throw new ArgumentNullException("Sale must be at least 2 weeks long");
            TourIds = tourIds;
            StartDate = startDate;
            EndDate = endDate;
            SalePercentage = salePercentage;   
        }
    }
}
