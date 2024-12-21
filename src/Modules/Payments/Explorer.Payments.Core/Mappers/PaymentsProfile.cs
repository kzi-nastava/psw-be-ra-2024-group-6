using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile()
    {
        CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price(src.Price)))
                .ReverseMap()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));


        CreateMap<ShoppingCartDto, ShoppingCart>()
            .IncludeAllDerived()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => new Price(src.Price)))
            .ReverseMap()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.TotalPrice.Amount));


        CreateMap<PurchaseTokenDto, PurchaseToken>().ReverseMap();

        CreateMap<WalletDto, Wallet>().ReverseMap();

        CreateMap<ProductDto, Product>()
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price(src.Price)))
                    .ForMember(dest => dest.ResourceTypeId, opt => opt.MapFrom(src => src.ResourceTypeId))
                    .ForMember(dest => dest.ResourceId, opt => opt.MapFrom(src => src.ResourceId ?? 0))
                    .ReverseMap()
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                    .ForMember(dest => dest.ResourceId, opt => opt.MapFrom(src => (long?)src.ResourceId));

        CreateMap<Coupon, CouponDto>().ReverseMap();

        CreateMap<PaymentRecord, PaymentRecordDto>()
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount)); // Assuming Price has a Value property

        // Mapping from PaymentRecordDto to PaymentRecord
        CreateMap<PaymentRecordDto, PaymentRecord>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price(src.Price))) // Assuming Price has a constructor that accepts a double
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Optional: Avoid overwriting default values with null
    }
}
