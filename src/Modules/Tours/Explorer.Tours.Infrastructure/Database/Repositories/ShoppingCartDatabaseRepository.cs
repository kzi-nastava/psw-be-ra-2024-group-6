using System;
using System.Collections.Generic;
using System.Linq;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ShoppingCartDatabaseRepository : IShoppingCartRepository
    {
        private readonly ToursContext _dbContext;

        public ShoppingCartDatabaseRepository(ToursContext dbContext)
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

        public ShoppingCart Get(long id)
        {
            var cart = _dbContext.ShoppingCarts.Where(t => t.Id == id)
                .Include(t => t.OrderItems!).FirstOrDefault();
            if (cart == null)
            {
                throw new KeyNotFoundException($"ShoppingCart not found: {id}");
            }
            return cart;
        }

    }
}
