using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class CouponService : CrudService<CouponDTO, Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IInternalTourPaymentService _tourPaymentService;

        public CouponService(ICrudRepository<Coupon> repository, ICouponRepository couponRepository, IInternalTourPaymentService tourPayment, IMapper mapper) :
            base(repository, mapper)
        {
            _couponRepository = couponRepository;
            _tourPaymentService = tourPayment;
        }

        public Result<CouponDTO> Get(long id)
        {
            return MapToDto(_couponRepository.Get(id));
        }



        public Result<List<CouponDTO>> GetAllByAuthorId(long id, long userId)
        {
            if(!CheckIfAuthorized(id, userId, null))
                return Result.Fail("User did not create this coupon.");
            else
            {
                return MapToDto(_couponRepository.GetAllByAuthorId(id));
            }
        }

        public Result<CouponDTO> Update(CouponDTO coupon, long userId)
        {
            if (!CheckIfAuthorized(coupon.AuthorId, userId, coupon.TourId))
                return Result.Fail("User did not create this coupon.");

            else
            {
                return MapToDto(_couponRepository.Update(MapToDomain(coupon)));
            }

        }

        public Result Delete(long id, long userId)
        {
            var coupon = Get(id);
            if (!CheckIfAuthorized(coupon.Value.AuthorId, userId, null))
                return Result.Fail("User did not create this coupon.");
            else
            {
                _couponRepository.Delete(id);
                return Result.Ok();
            }
        }




        private bool CheckIfAuthorized(long authorId, long userId, long? tourId)
        {
            var isAuthor = authorId == userId;
            if (tourId == null)
            {
                return isAuthor;
            }
            else
            {
                var isAuthorOfTour = _tourPaymentService.IsUserAuthor((long)tourId, userId);

                return isAuthor && isAuthorOfTour;
            }
        }
    }
}
