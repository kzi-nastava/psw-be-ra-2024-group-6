using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
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
       

        public BundleService(IBundleRepository bundleRepository, IMapper mapper) : base( mapper)
        {
            _bundleRepository = bundleRepository;
           
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

        public Result<List<BundleDto>> GetAll()
        {
            try
            {
                var bundles = _bundleRepository.GetAll();
                return MapToDto(bundles);
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
