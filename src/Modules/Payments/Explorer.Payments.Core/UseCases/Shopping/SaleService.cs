using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Payments.API.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;



namespace Explorer.Payments.Core.UseCases.Shopping
{
    internal class SaleService : CrudService<SaleDto, Sale>, ISaleService
    {
        private readonly ISaleRepository _commentRepository;
        private readonly IMapper _mapper;
        public SaleService(ISaleRepository commentRepository, ICrudRepository<Sale> repository, IMapper mapper) : base(repository, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }



    }
}
