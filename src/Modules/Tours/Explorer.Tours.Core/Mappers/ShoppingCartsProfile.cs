using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class ShoppingCartsProfile : Profile
    {
        public ShoppingCartsProfile()
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
        }
    }
}
