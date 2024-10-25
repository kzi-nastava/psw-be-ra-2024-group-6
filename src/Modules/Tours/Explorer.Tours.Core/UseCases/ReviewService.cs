using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class ReviewService : CrudService<ReviewDto, Review>, IReviewService
    {
        private readonly ITourReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewService(ITourReviewRepository reviewRepository, ICrudRepository<Review> repository, IMapper mapper) : base(repository, mapper) {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public IEnumerable<ReviewDto> GetAllReviews()
        {
            var reviews = _reviewRepository.GetAll(); 
            return reviews.Select(review => _mapper.Map<ReviewDto>(review)); 
        }

    }
}
