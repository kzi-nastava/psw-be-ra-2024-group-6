using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto,Tour>,ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ICrudRepository<Tour> crudRepository;
        private readonly ICheckpointService _checkpointService;
        private readonly IObjectService _objectService;
        private readonly IMapper mapper;

        public TourService(ICrudRepository<Tour> repository, IMapper mapper,ITourRepository tourRepository, IObjectService objectService,ICheckpointService checkpointService) : base(repository, mapper)
        {
            _tourRepository = tourRepository;
            crudRepository = repository;
            _objectService = objectService;
            _checkpointService = checkpointService;
            this.mapper = mapper;
        }

        public Result<TourCreateDto> CreateTour(TourCreateDto createTour)
        {
            try
            {
                // Mapirajte DTO u entitet ture
                Tour tour = mapper.Map<Tour>(createTour);

                // Kreirajte novu turu u repozitorijumu
                Tour newTour = crudRepository.Create(tour);

                

                // Vratite uspešan rezultat sa kreiranim DTO-om
                return Result.Ok(createTour);
            }
            catch (Exception ex)
            {
                // Vratite grešku u slučaju neuspeha
                return Result.Fail(ex.Message);
            }


        }

        public Result<List<TourDto>> GetByUserId(long userId)
        {
            try
            {
                var el = MapToDto(_tourRepository.GetByUserId(userId));
                return el;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

        }

        //public Result<TourDetailsDto> GetTourDetailsByTourId(long tourId)
        //{
            /*Tour tour = crudRepository.Get(tourId);

            TourDto tourDto = MapToDto(tour);

            List<CheckpointDto> Checkpoints = _checkpointService.GetByTourId(tourId).Value;
            List<ObjectDto> Objects = _objectService.GetByTourId(tourId).Value;

            foreach (CheckpointDto checkpointDto in Checkpoints)
            {
                new CheckpointReadDto(checkpointDto., checkpointDto.Name, checkpointDto.Description, checkpointDto.ImageUrl) 
            }
            */
            

        //}
    }
}
