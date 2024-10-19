using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.Stakeholders.API.Public.Administration;

namespace Explorer.Stakeholders.Core.UseCases.Administration
{
    public class ProblemService : CrudService<ProblemDto, Problem> , IProblemService
    {

        public ProblemService(ICrudRepository<Problem> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<PagedResult<ProblemDto>> GetByTourId(int tourid)
        {
            throw new NotImplementedException();
        }

        public Result<PagedResult<ProblemDto>> GetByTouristId(int touristid)
        {
            throw new NotImplementedException();
        }
    }
}
