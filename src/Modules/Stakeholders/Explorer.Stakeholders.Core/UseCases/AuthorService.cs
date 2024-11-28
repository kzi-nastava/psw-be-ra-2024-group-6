using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AuthorService : BaseService<PersonDto, Person>, IAuthorService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUserService _userService;
        private readonly IMapper mapper;

        public AuthorService(IPersonRepository personRepository, IUserService userService, IMapper mapper) : base(mapper)
        {
            this._personRepository = personRepository;
            this._userService = userService;
        }

        public Result<List<PersonDto>> GetMostPopularAuthors()
        {
            var authorsIds = _userService.GetAllAuthorsIds();
            var result = _personRepository.GetMostFollowedAuthors(authorsIds);
            return MapToDto(result);
        }
    }
}
