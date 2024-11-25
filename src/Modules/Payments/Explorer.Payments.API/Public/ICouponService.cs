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
        Result<CouponDTO> Get(long id);
        Result<List<CouponDTO>> GetAllByAuthorId(long id, long userId);
        Result<CouponDTO> Create(CouponDTO coupon);
        Result<CouponDTO> Update(CouponDTO coupon, long userId);
        Result Delete(long id, long userId);
    }
}
