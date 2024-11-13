using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class RatingDatabaseRepository : IRatingRepository
    {
        private readonly StakeholdersContext _dbContext;

        public RatingDatabaseRepository(StakeholdersContext dbContext)
        {

            _dbContext = dbContext;
        }
        public List<Rating> GetBestRatings(int page, int pageSize)
        {
            return _dbContext.Ratings
                .OrderByDescending(r => r.StarRating)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
