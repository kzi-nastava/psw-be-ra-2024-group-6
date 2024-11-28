using AutoMapper;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Dtos;

namespace Explorer.Payments.Core.Mappers
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleDto, Sale>().ReverseMap();
        }
    }
}
