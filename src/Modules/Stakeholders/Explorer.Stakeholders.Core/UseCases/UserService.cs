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
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;
        public UserService(ICrudRepository<User> crudRepository, IUserRepository userRepository, IMapper mapper) : base(crudRepository,mapper)
        {
            _crudRepository = crudRepository;
            _userRepository = userRepository;
            this.mapper = mapper;
        }

        public new Result<UserDto> Update(UserDto user)
        {
            var result = MapToDto(_crudRepository.Update(MapToDomain(user)));
            return result;
        }

        public PagedResult<UserDto> GetPaged()
        {
            int page = 1;       //fixed values, change if needed
            int pageSize = 10;
            var result = MapToDto(_crudRepository.GetPaged(page, pageSize));
            if(result.IsFailed)
            {
                throw new Exception("Failed to map");
            }
            var users = result.Value.Results;

            foreach(var userDto in users)
            {
                var email = _userRepository.GetUserEmail(userDto.Id);

                if(!string.IsNullOrEmpty(email))
                {
                    userDto.Email = email;
                }
                else
                {
                    userDto.Email = "N/A";
                }

            }

            return result.Value;
        }
    }
}
