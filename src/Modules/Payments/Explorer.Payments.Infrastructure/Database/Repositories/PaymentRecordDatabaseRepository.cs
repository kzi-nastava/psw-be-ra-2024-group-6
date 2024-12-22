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
}
