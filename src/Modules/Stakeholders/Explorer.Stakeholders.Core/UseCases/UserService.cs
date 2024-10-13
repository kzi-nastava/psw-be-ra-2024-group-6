using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : CrudService<UserDto, User>, IUserService
    {
        private readonly ICrudRepository<User> _crudRepository;
        private readonly IMapper mapper;
        public UserService(ICrudRepository<User> crudRepository, IMapper mapper) : base(crudRepository,mapper)
        {
            _crudRepository = crudRepository;
            this.mapper = mapper;
        }

        public Result<UserDto> Update(int id) //void??
        {
            User user = _crudRepository.Get(id); //might need to be separated into a separate function
            var result = MapToDto(_crudRepository.Update(user));
            return result;
        }

        public new PagedResult<UserDto> GetPaged(int page, int pageSize) //might not work not sure
        {
            var result = MapToDto(_crudRepository.GetPaged(page, pageSize));
            if(result.IsFailed)
            {
                throw new Exception("Failed to map");
            }
            return result.Value;
        }
    }
}
