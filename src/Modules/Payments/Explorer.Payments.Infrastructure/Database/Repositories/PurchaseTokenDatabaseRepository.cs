using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class PurchaseTokenDatabaseRepository : IPurchaseTokenRepository
{
    private readonly PaymentsContext _dbContext;

    public PurchaseTokenDatabaseRepository(PaymentsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public PurchaseToken Create(PurchaseToken token)
    {
        var pt = _dbContext.PurchaseTokens.Add(token).Entity;
        _dbContext.SaveChanges();
        return pt;
    }

    public List<PurchaseToken> GetByUserId(long id)
    {
        return _dbContext.PurchaseTokens
            .Where(t => t.UserId == id)
            .ToList();
    }


    public PurchaseToken GetByUserAndTour(long userId, long tourId)
    {
        //removed && t.isExpired == false 
        var result = _dbContext.PurchaseTokens
            .FirstOrDefault(t => t.UserId == userId && t.TourId == tourId);

        return result; // Ensure all code paths return a value
    }


    public PurchaseToken Update(PurchaseToken token)
    {
        try
        {
            var existingToken = _dbContext.PurchaseTokens.Find(token.Id);
            if (existingToken != null)
            {
                _dbContext.Entry(existingToken).State = EntityState.Detached;
            }

            _dbContext.Entry(token).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        return token;
    }
}
