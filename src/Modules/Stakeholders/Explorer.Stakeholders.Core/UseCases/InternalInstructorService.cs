using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalInstructorService: IInternalInstructorService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public InternalInstructorService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Metoda za dobavljanje jednog instruktora
        public Result<UserDto> Get(int instructorId)
        {
            var user = _userRepository.GetById(instructorId);
            return _mapper.Map<UserDto>(user);
        }

        // Metoda za dobavljanje više instruktora
        public Result<List<UserDto>> GetMany(List<int> instructorIds)
        {
            var users = _userRepository.GetByIds(instructorIds);
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }
    }
}
