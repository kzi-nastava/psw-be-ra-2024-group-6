using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Execution;

public interface IRoadTripExecutionService
{
    public Result<RoadTripExecutionDto> Create(RoadTripExecutionCreateDto roadTripExecutionDto, int userId);
    public Result<RoadTripExecutionDto> GetById(int roadTripExecutionId, int userId);
    public Result<ICollection<RoadTripExecutionDto>> GetByTouristId(int touristId);
    public Result<RoadTripExecutionDto> FinalizeRoadTripExecution(int roadTripExecutionId, int userId, string status);
    public Result<RoadTripExecutionDto> UpdateLocation(RoadTripExecutionDto roadTripExecutionDto);
    public Result<RoadTripExecutionDto> UpdateCompletion(int roadTripExecutionId);
}
