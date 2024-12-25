using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class PaymentRecordDatabaseRepository : IPaymentRecordRepository
{
    private readonly PaymentsContext _dbContext;

    public PaymentRecordDatabaseRepository(PaymentsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public PaymentRecord Create(PaymentRecord record)
    {
        var r = _dbContext.PaymentRecords.Add(record).Entity;
        _dbContext.SaveChanges();
        return r;
    }

    public List<PaymentRecord> GetByTourIds(List<long> tourIds)
    {
        if (tourIds == null || !tourIds.Any())
        {
            return new List<PaymentRecord>(); 
        }

        return _dbContext.PaymentRecords
            .Where(pr => tourIds.Contains(pr.ResourceId) && pr.ResourceTypeId == 1) 
            .ToList(); 
    }

    public List<PaymentRecord> GetByBundleId(long bundleId)
    {
        if(bundleId == null)
        {
            return new List<PaymentRecord>();
        }
        return _dbContext.PaymentRecords
            .Where(pr => pr.ResourceId == bundleId && pr.ResourceTypeId == 2)
            .ToList();
    }

    public List<PaymentRecord> GetByTourId(long tourId)
    {
        return _dbContext.PaymentRecords.Where(pr => pr.ResourceId == tourId && pr.ResourceTypeId == 1).ToList();
    }

    public List<PaymentRecord> GetByBundleIds(List<long> bundleIds)
    {
        return _dbContext.PaymentRecords.Where(pr => bundleIds.Contains(pr.ResourceId) && pr.ResourceTypeId == 2).ToList();
    }

    public List<PaymentRecord> GetByCustomerAndTourIds(List<long> customerIds, List<int> tourIds)
    {
        return _dbContext.PaymentRecords.Where(pr => customerIds.Contains(pr.TouristId) && tourIds.Contains((int)pr.ResourceId) && pr.ResourceTypeId == 1).ToList();
    }

    public List<PaymentRecord> GetByCustomerIdsAndTourId(List<long> customerIds, int tourId)
    {
        return _dbContext.PaymentRecords.Where(pr => customerIds.Contains(pr.TouristId) && pr.ResourceId == tourId && pr.ResourceTypeId == 1).ToList();
    }

}
