using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class InternalPaymentRecordService: CrudService<PaymentRecordDto, PaymentRecord> ,IInternalPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly IMapper mapper;

        public InternalPaymentRecordService(ICrudRepository<PaymentRecord> crudRepository, IPaymentRecordRepository paymentRecordRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            this._paymentRecordRepository = paymentRecordRepository;
            this.mapper = mapper;
        }

        public int CalculateToursSalesByAuthor(List<long> tourIds)
        {
            return _paymentRecordRepository.GetByTourIds(tourIds).Count();

        }

        public int CountBundleSalesByAuthor(long bundleId)
        {
            return _paymentRecordRepository.GetByBundleId(bundleId).Count();
        }

        public int CalculateTourSales(long tourId, List<long> bundleIds)
        {
            int sales = _paymentRecordRepository.GetByTourId(tourId).Count();
            var customerIds = _paymentRecordRepository.GetByBundleIds(bundleIds).Select(pr => pr.TouristId).ToList();
            int correction = _paymentRecordRepository.GetByCustomerIdsAndTourId(customerIds, (int)tourId).Count();
            sales += _paymentRecordRepository.GetByBundleIds(bundleIds).Count() - correction;
            return sales;
        }

        public List<long> GetCustomerIdsByBundleId(long bundleId)
        {
            return _paymentRecordRepository.GetByBundleId(bundleId).Select(pr => pr.TouristId).ToList();
        }

        public int CountAlreadyBoughtTours(List<long> customerIds, List<int> tourIds)
        {
            return _paymentRecordRepository.GetByCustomerAndTourIds(customerIds, tourIds).Count();
        }

    }
}
