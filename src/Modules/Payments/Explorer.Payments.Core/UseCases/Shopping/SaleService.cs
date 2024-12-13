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
using Explorer.Tours.API.Internal;



namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class SaleService : CrudService<SaleDto, Sale>, ISaleService
    {
        private readonly ICrudRepository<Sale> _saleRepository;
        private readonly IMapper _mapper;
        private readonly IInternalTourPaymentService _tourPaymentService;
        public SaleService(ISaleRepository saleRepository, ICrudRepository<Sale> repository, IInternalTourPaymentService tourPaymentService, IMapper mapper) : base(repository, mapper)
        {
            _saleRepository = repository;
            _tourPaymentService = tourPaymentService;
            _mapper = mapper;
        }



    }
}
