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
    public class PersonService : BaseService<PersonDto,Person>, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper mapper;

        public PersonService(IPersonRepository personRepository,IMapper mapper) : base(mapper)
        {
            _personRepository = personRepository;
            this.mapper = mapper;
        }

        Result<PersonDto> IPersonService.GetByUserId(int id)
        {
            var el = MapToDto(_personRepository.GetByUserId(id));
            return el;
        }

        Result<PersonDto> IPersonService.Update(PersonDto person)
        {
            var el = _personRepository.Update(MapToDomain(person));
            return MapToDto(el);
        }
    }
}
