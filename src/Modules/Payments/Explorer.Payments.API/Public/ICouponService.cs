using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Dtos;

namespace Explorer.Payments.API.Public
{
    public interface ICouponService
    {
        Result<CouponDto> GetById(long id);

        Result<CouponDto> GetByCode(string code);
        Result<List<CouponDto>> GetAllByAuthorId(long userId);
        Result<CouponDto> Create(CouponDto coupon);
        Result<CouponDto> Update(CouponDto coupon, long userId);
        Result Delete(long id, long userId);
    }
}
