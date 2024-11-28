using System;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain
{
    public class Coupon : Entity
    {
        public string Code { get; private set; }
        public double DiscountPercentage { get; private set; }
        public long AuthorId { get; private set; }
        public long? TourId { get; private set; }
        public DateTime? ExpiresDate { get; private set; }

        public Coupon()
        {
        }

        public Coupon(string code, double discount, long authorId, long? tourId)
        {
            Code = code;
            DiscountPercentage = discount;
            AuthorId = authorId;
            TourId = tourId;
            Validate();
        }

        public void UpdateDiscount(double discount)
        {
            Validate();
            DiscountPercentage = discount;
        }

        public void UpdateTour(long? tourId)
        {
            TourId = tourId;
        }

        public void UpdateExpirationDate(DateTime? expiresDate)
        {
            ExpiresDate = expiresDate;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Code) || Code.Length != 8)
                throw new ArgumentException("Code must be exactly 8 characters long and not empty.");

            if (DiscountPercentage <= 0 || DiscountPercentage > 100)
                throw new ArgumentException("Discount must be a positive number between 0 and 100.");
        }

        public bool IsUserAuthorOfCoupon(long userId, long authorId)
        {
            return userId == authorId;
        }
    }
}