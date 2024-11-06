using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class ReviewService : CrudService<ReviewDto, Review>, IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository reviewRepository, ICrudRepository<Review> repository, IMapper mapper) : base(repository, mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public Result<ReviewDto> CreateWithDateParser(ReviewDto reviewDto)
        {
            reviewDto.TourDate = DateTime.SpecifyKind(reviewDto.TourDate, DateTimeKind.Utc);
            reviewDto.ReviewDate = DateTime.SpecifyKind(reviewDto.ReviewDate, DateTimeKind.Utc);

            try
            {
                var reviewEntity = _mapper.Map<Review>(reviewDto);
                var result = _reviewRepository.Create(reviewEntity);

                return result.IsSuccess
                    ? Result.Ok(_mapper.Map<ReviewDto>(reviewEntity))
                    : Result.Fail(result.Errors);
            }
            catch (Exception ex) { return  Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message); }
        }


        public IEnumerable<ReviewDto> GetAllReviews()
        {
            var reviews = _reviewRepository.GetAll();
            return reviews.Select(MapToDto);
        }

        public IEnumerable<ReviewDto> GetReviewsFromTourId(long tourId)
        {
            var reviews = _reviewRepository.GetReviewsForTour(tourId);
            return reviews.Select(MapToDto);

        }
    }

}
