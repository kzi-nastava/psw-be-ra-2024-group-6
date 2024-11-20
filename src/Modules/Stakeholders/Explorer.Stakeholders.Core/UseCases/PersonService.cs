using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Persons;
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
        private readonly IImageRepository _imageRepository;
        private readonly IMapper mapper;

        public PersonService(IPersonRepository personRepository,IImageRepository imageRepository,IMapper mapper) : base(mapper)
        {
            _personRepository = personRepository;
            _imageRepository = imageRepository;
            this.mapper = mapper;
        }

        public Result<PersonDto> AddFollower(int followerId, int userId)
        {
            Person follower  = _personRepository.GetByUserId(userId);
            follower.AddFollowing(followerId);
            _personRepository.Update(follower);

            Person following = _personRepository.GetByUserId(followerId);
            following.AddFollower(userId);
            _personRepository.Update(following);

            return MapToDto(follower);


        }

        public Result<List<PersonDto>> GetFollowers(int userId)
        {
            try
            {
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

                if(el.ImageId != null)  //if the image exists it fills the dto with it
                {
                    var img = _imageRepository.Get(el.ImageId.Value);
                    el.ImageName = img.name;
                    el.ImageData = img.data;
                }

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
                if(person.ImageId != null && person.ImageName != null && person.ImageData != null) //updates the image if it already exists
                {
                    var image = mapper.Map<Image>(new ImageDto(person.ImageId.Value, person.ImageName, person.ImageData));
                    _imageRepository.Update(image);
                }
                if(person.ImageId == null && person.ImageName != null && person.ImageData != null) //creates a new image in case it doesnt exist
                {
                    var image = mapper.Map<Image>(new ImageDto(null, person.ImageName, person.ImageData));
                    _imageRepository.Add(image);
                    person.ImageId = image.Id;
                }
                var newPerson = MapToDomain(person);
                if(newPerson.ImageId == 0)
                {
                    newPerson.ImageId = null;
                }
                var el = _personRepository.Update(newPerson);
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
