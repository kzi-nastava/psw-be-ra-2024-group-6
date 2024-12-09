using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AuthorService : BaseService<PersonDto, Person>, IAuthorService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IUserService _userService;
        private readonly IMapper mapper;

        public AuthorService(IPersonRepository personRepository, IImageRepository imageRepository, IUserService userService, IMapper mapper) : base(mapper)
        {
            this._imageRepository = imageRepository;
            this._personRepository = personRepository;
            this._userService = userService;
        }

        public Result<List<PersonDto>> GetMostPopularAuthors()
        {
            try
            {
                var authorsIds = _userService.GetAllAuthorsIds();
                var result = _personRepository.GetMostFollowedAuthors(authorsIds);
                List<PersonDto> resultForward = MapToDto(result).Value;

                foreach (var author in resultForward)
                {
                    if (author.ImageId != null)
                    {
                        Image image = _imageRepository.Get((long)author.ImageId);

                        if (image != null)
                        {
                            author.ImageData = image.data;
                            author.ImageName = image.name;
                        }
                    }
                }


                return resultForward;
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
