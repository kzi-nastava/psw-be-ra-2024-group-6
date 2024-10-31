using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Infrastructure.Database;

namespace Explorer.Tours.Infrastructure.Database.Repositories;
public class ReviewDatabaseRepository : IReviewRepository
{
    private readonly ToursContext _dbContext;

    public ReviewDatabaseRepository(ToursContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Review> GetAll()
    {
        return _dbContext.Reviews.ToList(); 
    }

}

