using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ProblemTourService : IInternalProblemTourAuthorService
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;

        public ProblemTourService(IMapper mapper, ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
            this.mapper = mapper;
        }
        public Result<TourDto> GetTour(long tourId)
        {
            Tour tour = tourRepository.Get(tourId);
            return mapper.Map<TourDto>(tour);
        }
    }
}
