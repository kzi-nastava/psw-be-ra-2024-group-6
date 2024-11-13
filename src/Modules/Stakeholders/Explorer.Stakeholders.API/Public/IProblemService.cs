using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public.Administration
{
    public interface IProblemService
    {
        Result<PagedResult<ProblemDto>> GetAll(long userId);
        Result<PagedResult<ProblemDto>> GetPaged(int page, int pageSize);
        Result<ProblemDto> Create(ProblemDto equipment);
        Result<ProblemDto> Update(ProblemDto equipment, int userId);
        Result<ProblemDto> UpdateDueDate(ProblemDto equipment, int userId);
        Result Delete(int id);
        Result<ProblemDto> SendMessage(int userId1, ProblemDto prob,ProblemMessageDto message);
    }
}
