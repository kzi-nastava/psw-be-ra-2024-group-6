using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class RatingService : CrudService<RatingDto, Rating>, IRatingService
    {
        private IPersonService _personService;
        private IMapper _mapper;
        private IRatingRepository _ratingRepository;

        public RatingService(ICrudRepository<Rating> repository, IPersonService personService, IMapper mapper,IRatingRepository ratingRepository) : base(
            repository, mapper)
        {
            this._mapper = mapper;
            this._personService = personService;
            this._ratingRepository = ratingRepository;
        }

        public Result<List<RatingReadDto>> GetBestPaged(int page, int pageSize)
        {
            List<RatingReadDto> ratingDtos = new List<RatingReadDto>();
            var ratings = _ratingRepository.GetBestRatings(page, pageSize);
            foreach (var rating in ratings)
            {
                PersonDto person = _personService.GetByUserId(rating.UserId).Value;
                ratingDtos.Add(new RatingReadDto
                {
                    Id = rating.Id,
                    StarRating =(int) rating.StarRating,
                    Comment = rating.Comment,
                    PostedAt = rating.PostedAt,
                    Name = person.Name,
                    Surname = person.Surname,
                    PictureURL = person.PictureURL,
                    PeopleId = person.Id
                });
            }

            return ratingDtos;
        }
    }
}
