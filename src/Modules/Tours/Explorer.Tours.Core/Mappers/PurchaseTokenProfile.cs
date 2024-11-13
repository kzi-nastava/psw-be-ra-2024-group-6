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
    public class PurchaseTokenProfile : Profile
    {
        public PurchaseTokenProfile() 
        {
            CreateMap<PurchaseTokenDto, PurchaseToken>().ReverseMap();
        }
    }
}
