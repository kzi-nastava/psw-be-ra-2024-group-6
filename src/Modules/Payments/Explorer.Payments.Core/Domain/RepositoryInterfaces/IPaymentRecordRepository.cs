using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface IPaymentRecordRepository
{
    public PaymentRecord Create(PaymentRecord record);
    public List<PaymentRecord> GetByTourIds(List<long> tourIds);

    public List<PaymentRecord> GetByBundleId(long bundleId);

    public List<PaymentRecord> GetByTourId(long tourId);
    public List<PaymentRecord> GetByBundleIds(List<long> bundleIds);
    public List<PaymentRecord> GetByCustomerAndTourIds(List<long> customerIds, List<int> tourIds);
    public List<PaymentRecord> GetByCustomerIdsAndTourId(List<long> customerIds, int tourId);


}
