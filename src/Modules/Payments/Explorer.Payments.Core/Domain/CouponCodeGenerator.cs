using System;

namespace Explorer.Payments.Core.Domain
{
    public class CouponCodeGenerator
    {
        private readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public CouponCodeGenerator() { }

        public string GenerateCouponCode()
        {
            var timestamp = DateTime.UtcNow.Ticks; 
            var random = new Random((int)(timestamp & 0xFFFFFFF)); 
            var couponCode = new char[8];

            for (int i = 0; i < couponCode.Length; i++)
            {
                couponCode[i] = _chars[random.Next(_chars.Length)];
            }

            return new string(couponCode);
        }
    }
}