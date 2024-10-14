using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class RequiredEquipmentService : BaseService<RequiredEquipmentDto, RequiredEquipment>, IRequiredEquipmentService
    {
        private readonly IRequiredEquipmentRepository _requiredEquipmentRepository;
        public RequiredEquipmentService(IRequiredEquipmentRepository requiredEquipmentRepository, IMapper mapper) : base(mapper)
        {
            _requiredEquipmentRepository = requiredEquipmentRepository;
        }
        public Result<RequiredEquipmentDto> Create(RequiredEquipmentDto requiredEquipment)
        {
            try
            {
                var result = _requiredEquipmentRepository.Create(MapToDomain(requiredEquipment));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                return _requiredEquipmentRepository.Delete(id) ? Result.Ok() : Result.Fail(FailureCode.NotFound);
            }
            catch (ArgumentNullException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<RequiredEquipmentDto>> GetAllByTour(int tourId)
        {
            try
            {
                var result = _requiredEquipmentRepository.GetAllByTour(tourId).ToList();
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
