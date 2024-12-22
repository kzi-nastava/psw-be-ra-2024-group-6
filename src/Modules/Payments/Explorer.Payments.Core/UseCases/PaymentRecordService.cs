using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class PaymentRecordService : CrudService<PaymentRecordDto,PaymentRecord> , IPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly IMapper mapper;
        private readonly ICrudRepository<PaymentRecord> _crudRepository;

        public PaymentRecordService(ICrudRepository<PaymentRecord> crudRepository,IPaymentRecordRepository paymentRecordRepository, IMapper mapper) : base(crudRepository,mapper)
        {
            this._crudRepository = crudRepository;
            _paymentRecordRepository = paymentRecordRepository;
            this.mapper = mapper;
        }

        public Result<PaymentRecordDto> Create(PaymentRecord record)
        {
            var p = _paymentRecordRepository.Create(record);
            return MapToDto(mapper.Map<PaymentRecord>(p));
        }
    }

    
}
