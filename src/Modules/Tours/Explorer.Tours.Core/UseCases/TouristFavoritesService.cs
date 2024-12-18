using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class TouristFavoritesService : ITouristFavoritesService
    {
        private readonly ITouristFavoritesRepository _touristFavoritesRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IMapper _mapper;

        public TouristFavoritesService(ITouristFavoritesRepository touristFavoritesRepository, IMapper mapper, ICheckpointRepository checkpointRepository)
        {
            _touristFavoritesRepository = touristFavoritesRepository;
            _checkpointRepository = checkpointRepository;
            _mapper = mapper;
        }

        public Result<TouristFavoritesDto> GetByTouristId(int touristId)
        {
            var touristFavorites = _touristFavoritesRepository.GetByTouristId(touristId);
            if (touristFavorites == null)
                return Result.Fail(FailureCode.NotFound).WithError("Tourist does not have favorites.");
            var checkpoints = new List<CheckpointReadDto>();
            foreach (var id in touristFavorites.FavoriteCheckpointIds)
            {
                var checkpoint = _checkpointRepository.Get(id);
                checkpoints.Add(_mapper.Map<CheckpointReadDto>(checkpoint));
            }

            var dto = new TouristFavoritesDto(touristFavorites.Id, touristFavorites.TouristId, checkpoints);
            return dto;
        }
    }
}
