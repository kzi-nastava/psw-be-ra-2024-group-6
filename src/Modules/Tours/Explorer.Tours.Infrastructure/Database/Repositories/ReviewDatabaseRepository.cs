using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;

namespace Explorer.Tours.Infrastructure.Database.Repositories;
public class ReviewDatabaseRepository : IReviewRepository
{
    private readonly ToursContext _dbContext;

    public ReviewDatabaseRepository(ToursContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Result<Review> Create(Review review)
    {
        _dbContext.Add(review);

        var changes = _dbContext.SaveChanges();
        return changes > 0 ? Result.Ok(review) : Result.Fail("Failed to create and save the review.");
    }


    public IEnumerable<Review> GetAll()
    {
        return _dbContext.Reviews.ToList(); 
    }

    public IEnumerable<Review> GetReviewsForTour(long tourId)
    {
        return _dbContext.Reviews.Where(review => review.TourId == tourId).ToList();
    }
}

