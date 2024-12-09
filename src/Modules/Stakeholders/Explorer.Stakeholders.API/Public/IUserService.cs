using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserService
    {
        public PagedResult<UserDto> GetPaged();
        public Result<UserDto> Update(UserDto user);

        public Result<UserDto> GetWithoutPassword(long id);

        public Result<UserDto> Get(long id);
        public Result<string> GetUserRole(long id);
        public List<long> GetAllAuthorsIds();

    }
}
