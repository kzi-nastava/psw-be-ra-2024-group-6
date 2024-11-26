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
        Result<CouponDto> Get(long id);
        Result<List<CouponDto>> GetAllByAuthorId(long id, long userId);
        Result<CouponDto> Create(CouponDto coupon, long userId);
        Result<CouponDto> Update(CouponDto coupon, long userId);
        Result Delete(long id, long userId);
    }
}
