using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ICouponRepository : ICrudRepository<Coupon>
    {
        List<Coupon> GetAllByAuthorId(long authorId);
    }
}
