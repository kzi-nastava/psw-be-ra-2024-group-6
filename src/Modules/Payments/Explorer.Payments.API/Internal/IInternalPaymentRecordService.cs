using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal
{
    public interface IInternalPaymentRecordService
    {
        public int CalculateToursSalesByAuthor(List<long> tourIds);
        public int CountBundleSalesByAuthor(long bundleId);
        public int CalculateTourSales(long tourId, List<long> bundleIds);
        public List<long> GetCustomerIdsByBundleId(long bundleId);
        public int CountAlreadyBoughtTours(List<long> customerIds, List<int> tourIds);
    }
}
