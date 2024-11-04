using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.ProfileNotifications;
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

        public Result<PersonDto> AddFollower(int followerId, int userId)
        {
            Person follower  = _personRepository.GetByUserId(userId);
            follower.AddFollower(followerId);
            _personRepository.Update(follower);

            Person following = _personRepository.GetByUserId(followerId);
            following.AddFollowing(userId);
            _personRepository.Update(following);

            return MapToDto(follower);


        }

        public Result<List<PersonDto>> GetFollowers(int userId)
        {
            try
            {
                // Dohvati korisnika na osnovu userId
                var person = _personRepository.GetByUserId(userId);
                if (person == null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("User not found");
                }

                // Pronađi sve korisnike koji prate trenutnog korisnika
                var followers = person.Followers.Select(f => _personRepository.GetByUserId(f.PersonId)).ToList();

                // Mapiraj pronađene korisnike u DTO objekte
                var followerDtos = followers.Select(MapToDto).ToList();

                return Result.Ok(followerDtos);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        Result<PersonDto> IPersonService.GetByUserId(int id)
        {
            try
            {
                var el = MapToDto(_personRepository.GetByUserId(id));
                return el;
            }
            catch(KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch(ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        Result<PersonDto> IPersonService.Update(PersonDto person)
        {
            try
            {
                var el = _personRepository.Update(MapToDomain(person));
                return MapToDto(el);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);

            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);

            }

        }


    }
}
