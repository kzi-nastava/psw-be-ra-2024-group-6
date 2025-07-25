using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class BundleService : BaseService<BundleDto,Bundle> , IBundleService
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IInternalPurchaseTokenService _purchaseTokenRepository;
        private readonly IInternalWalletService _walletRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly ICheckpointRepository _checkpoinRepository;
        public BundleService(IBundleRepository bundleRepository,
            IInternalPurchaseTokenService purchaseTokenRepository
            ,IInternalWalletService  walletRepository,
            ITourRepository tourRepository,
            IPaymentRecordService paymentRecordService ,
            ICheckpointRepository checkpointRepository,
            IMapper mapper) : base( mapper)
        {
            _bundleRepository = bundleRepository;
            _purchaseTokenRepository = purchaseTokenRepository;
            _walletRepository = walletRepository;
            _tourRepository = tourRepository;
            _paymentRecordService = paymentRecordService;
            _checkpoinRepository = checkpointRepository;
           
        }

        public Result<BundleDto> Buy(BundleDto bundle,int userId)
        {
            try
            {
                WalletDto wallet = _walletRepository.GetByUserId(userId).Value;
                if(wallet.AdventureCoins < bundle.Price)
                {
                    return Result.Fail(FailureCode.Forbidden).WithError("Not enough coins.");
                }

                foreach (int tour in bundle.TourIds)
                {
                    
                    PurchaseTokenDto dto = new PurchaseTokenDto
                    {
                        UserId = userId,
                        TourId = tour,
                        PurchaseDate = DateTime.UtcNow,
                        isExpired = false

                    };
                    _purchaseTokenRepository.Create(dto);
                }

                PriceDto price = new PriceDto
                {
                    Amount = bundle.Price
                };

                PaymentRecordDto record = new PaymentRecordDto
                {
                    TouristId = userId,
                    ResourceId = (long)bundle.Id,
                    ResourceTypeId = 2,
                    Price = bundle.Price,
                    PaymentDate = DateTime.UtcNow,

                };
                
                _paymentRecordService.Create(record);


                wallet.AdventureCoins -= (long)bundle.Price;
                _walletRepository.Update(wallet);
                return bundle;
            }
            catch (ArgumentException ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");

            }
        }
        public Result<List<BundleDto>> GetAll()
        {
            try
            {
                var bundles = _bundleRepository.GetAll();
                return MapToDto(bundles);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BundleDto> Update(BundleDto bundle)
        {
            try
            {

                Bundle bundle_m = _bundleRepository.Update(MapToDomain(bundle));
                return MapToDto(bundle_m);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("bundle not found");
            }
        }

        public Result<BundleDto> Publish(long bundleId)
        {
            try
            {
                Bundle bundle = _bundleRepository.GetById(bundleId);
                if (!bundle.Publish())
                    return Result.Fail("publish failed");
                _bundleRepository.Update(bundle);
                return MapToDto(bundle);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("bundle not found");
            }


        }

        public Result<BundleDto> Archive(long bundleId)
        {
            try
            {
                Bundle bundle = _bundleRepository.GetById(bundleId);
                if (!bundle.Archive())
                    return Result.Fail("archiving failed");
                _bundleRepository.Update(bundle);
                return MapToDto(bundle);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("bundle not found");
            }


        }

        public Result<List<BundleDto>> GetAllByUserId(long userId)
        {
            try
            {
                var bundles = _bundleRepository.GetAll();
                List<Bundle> bundles_filtered = new List<Bundle>();
                foreach (var bundle in bundles)
                {
                    if(bundle.AuthorId == userId)
                        bundles_filtered.Add(bundle);
                }

                return MapToDto(bundles_filtered);
            }
            catch(Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BundleDto> Create(BundleDto bundle)
        {
            try
            {
                Tour tour = _tourRepository.Get(bundle.TourIds[0]);
                List<Checkpoint> checkpoints = _checkpoinRepository.GetByTourId(tour.Id);
                bundle.ImageData = checkpoints[0].ImageData;
                var result = _bundleRepository.Create(MapToDomain(bundle));
                return MapToDto(result);
            }
            catch(ArgumentException ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");

            }
        }

        public List<long> GetBundleIdsByTourId(long tourId)
        {
            return _bundleRepository.GetByTourId(tourId).Select(b => b.Id).ToList();
        }
    }
}
