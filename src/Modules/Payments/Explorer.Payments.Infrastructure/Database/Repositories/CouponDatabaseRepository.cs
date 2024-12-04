using System;
using System.Collections.Generic;
using System.Linq;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class CouponDatabaseRepository : ICouponRepository
    {
        private readonly PaymentsContext _dbContext;

        public CouponDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }


        public PagedResult<Coupon> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Coupon Get(long id)
        {
            var coupon = _dbContext.Coupons
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (coupon == null)
            {
                throw new KeyNotFoundException($"Coupon with id {id} not found.");
            }

            return coupon;
        }

        public Coupon Create(Coupon entity)
        {
            var createdCoupon = _dbContext.Coupons.Add(entity).Entity;
            _dbContext.SaveChanges();
            return createdCoupon;
        }

        public Coupon Update(Coupon entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return entity;
        }

        public void Delete(long id)
        {
            var coupon = Get(id); // Ensure it exists or throw exception
            _dbContext.Coupons.Remove(coupon);
            _dbContext.SaveChanges();
        }

        public List<Coupon> GetAllByAuthorId(long authorId)
        {
            var coupons = _dbContext.Coupons
                .Where(c => c.AuthorId == authorId)
                .ToList();


            return coupons;
        }


    }
}
