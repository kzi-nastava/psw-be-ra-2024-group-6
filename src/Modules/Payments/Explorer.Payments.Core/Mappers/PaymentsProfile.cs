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

        CreateMap<SaleDto, Sale>().ReverseMap();
    }
}
