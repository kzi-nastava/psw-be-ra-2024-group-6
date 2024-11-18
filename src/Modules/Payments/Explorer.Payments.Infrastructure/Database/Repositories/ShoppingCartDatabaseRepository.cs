using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class ShoppingCartDatabaseRepository : IShoppingCartRepository
{
    private readonly PaymentsContext _dbContext;

    public ShoppingCartDatabaseRepository(PaymentsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ShoppingCart Create(ShoppingCart cart)
    {
        var sc = _dbContext.ShoppingCarts.Add(cart).Entity;
        _dbContext.SaveChanges();
        return sc;
    }

    public ShoppingCart Update(ShoppingCart cart)
    {
        try
        {
            _dbContext.Entry(cart).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        return cart;
    }

    public void Delete(long id)
    {
        var entity = Get(id);
        _dbContext.ShoppingCarts.Remove(entity);
        _dbContext.SaveChanges();
    }


    public ShoppingCart? Get(long id)
    {
        var cart = _dbContext.ShoppingCarts
            .Where(t => t.Id == id)
            .Include(t => t.OrderItems)
            .FirstOrDefault();

        return cart;
    }

    public ShoppingCart? GetByUserId(long id)
    {
        var cart = _dbContext.ShoppingCarts.Where(t => t.UserId == id)
            .Include(t => t.OrderItems)
            .FirstOrDefault();

        return cart;
    }
}
