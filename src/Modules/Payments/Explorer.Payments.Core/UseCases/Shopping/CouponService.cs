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
    public class CouponService : CrudService<CouponDto, Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IInternalTourPaymentService _tourPaymentService;
        private readonly CouponCodeGenerator _codeGenerator;

        public CouponService(ICrudRepository<Coupon> repository, ICouponRepository couponRepository, IInternalTourPaymentService tourPayment, IMapper mapper) :
            base(repository, mapper)
        {
            _couponRepository = couponRepository;
            _tourPaymentService = tourPayment;
            _codeGenerator = new CouponCodeGenerator();
        }

        public Result<CouponDto> GetById(long id)
        {
            return MapToDto(_couponRepository.Get(id));
        }

        public Result<CouponDto> GetByCode(string code)
        {
            throw new NotImplementedException();
        }


        public Result<List<CouponDto>> GetAllByAuthorId(long userId)
        {
/*            if(!CheckIfAuthorized(id, userId, null))
                return Result.Fail(FailureCode.Forbidden).WithError("User did not create this coupon.");*/

                
            return MapToDto(_couponRepository.GetAllByAuthorId(userId));

        }

        public Result<CouponDto> Create(CouponDto coupon)
        {
            if (coupon.TourId != null && !IsUserAuthorOfTour(coupon.AuthorId, (long)coupon.TourId))
                return Result.Fail(FailureCode.Forbidden).WithError("User is not author of specified tour");

            coupon.Code = _codeGenerator.GenerateCouponCode();


            return MapToDto(_couponRepository.Create(MapToDomain(coupon)));
        }

        public Result<CouponDto> Update(CouponDto couponDto, long userId)
        {
            var coupon = MapToDomain(couponDto);

            if (!coupon.IsUserAuthorOfCoupon(userId, coupon.AuthorId))
                return Result.Fail(FailureCode.Forbidden).WithError("User did not create this coupon.");

            if (coupon.TourId != null && !IsUserAuthorOfTour(coupon.AuthorId, (long)coupon.TourId))
                return Result.Fail(FailureCode.Forbidden).WithError("User is not author of specified tour");

            var existingCoupon = _couponRepository.Get(coupon.Id);
            if (existingCoupon == null)
                return Result.Fail(FailureCode.NotFound).WithError("Coupon not found.");

/*            if (existingCoupon.Code != coupon.Code)
                return Result.Fail(FailureCode.InvalidArgument).WithError("Coupon code cannot be modified.");*/

            // Ažurirajte polja koristeći metode entiteta
            existingCoupon.UpdateDiscount(coupon.DiscountPercentage);
            existingCoupon.UpdateTour(coupon.TourId);
            existingCoupon.UpdateExpirationDate(coupon.ExpiresDate);

            _couponRepository.Update(existingCoupon);

            return MapToDto(existingCoupon);
        }



        public Result Delete(long couponId, long userId)
        {
            var coupon = _couponRepository.Get(couponId);
            if (!coupon.IsUserAuthorOfCoupon(userId, couponId))
                return Result.Fail(FailureCode.Forbidden).WithError("User did not create this coupon.");

            _couponRepository.Delete(couponId);
            return Result.Ok();
            
        }


        private bool IsUserAuthorOfTour(long authorId, long tourId)
        {
            return _tourPaymentService.IsUserAuthor(tourId, authorId);
        }
    }
}
