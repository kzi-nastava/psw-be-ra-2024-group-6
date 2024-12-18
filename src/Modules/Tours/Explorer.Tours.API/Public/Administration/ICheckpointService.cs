using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ICheckpointService 
    {
        Result<PagedResult<CheckpointDto>> GetPaged(int page, int pageSize); 
        Result<CheckpointDto> Update(CheckpointDto equipment);
        Result Delete(int id);
        Result<List<CheckpointReadDto>> GetByTourId(long tourId);
        Result<CheckpointDto> Create(CheckpointCreateDto checkpointCreateDto);

        Result<CheckpointReadDto> CreatePublicCheckpoint(CheckpointDto checkpointCreateDto, long userId);

        Result<CheckpointDto> Get(long id);


        Result<List<CheckpointReadDto>> GetPendingPublicCheckpoints();

        Result<List<DestinationDto>> GetMostPopularDestinations();
        List<int> GetTourIdsForDestination(string city, string country, int page, int pageSize);
        Result<List<CheckpointDto>> GetNearbyPublicCheckpoints(double latitude, double longitude, double radius);

        public Result<CheckpointDto> Get(long id);

        Result<CheckpointReadDto> ApproveCheckpointRequest(long checkpointId, long adminId);
        Result<CheckpointReadDto> RejectCheckpointRequest(long checkpointId, string comment, long adminId);
    }
}
