using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
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
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;
        private readonly IWalletRepository _walletRepository;

        public BundleService(IBundleRepository bundleRepository,IPurchaseTokenRepository purchaseTokenRepository,IWalletRepository  walletRepository, IMapper mapper) : base( mapper)
        {
            _bundleRepository = bundleRepository;
            _purchaseTokenRepository = purchaseTokenRepository;
            _walletRepository = walletRepository;
           
        }

        public Result<BundleDto> Buy(BundleDto bundle,int userId)
        {
            try
            {
                Wallet wallet = _walletRepository.GetByUserId(userId);
                if(wallet.AdventureCoins < bundle.Price)
                {
                    return Result.Fail(FailureCode.Forbidden).WithError("Not enough coins.");
                }

                foreach (int tour in bundle.TourIds)
                {
                    PurchaseToken token = new PurchaseToken(userId, tour,DateTime.UtcNow,false);
                    _purchaseTokenRepository.Create(token);
                }

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

                var result = _bundleRepository.Create(MapToDomain(bundle));
                return MapToDto(result);
            }
            catch(ArgumentException ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");

            }
        }
    }
}
