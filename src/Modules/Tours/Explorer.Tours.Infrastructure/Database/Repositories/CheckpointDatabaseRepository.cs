using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class CheckpointDatabaseRepository : ICheckpointRepository
    {
        private readonly ToursContext _dbContext;

        public CheckpointDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Checkpoint Create(Checkpoint checkpoint)
        {
            Checkpoint ch= _dbContext.Checkpoints.Add(checkpoint).Entity;
            _dbContext.SaveChanges();
            return ch;

        }

        public List<Checkpoint> GetByTourId(long tourId)
        {
                return _dbContext.Checkpoints
                    .Where(c => c.TourId == tourId)
                    .ToList();
        }

        public Checkpoint Get(long Id)
        {
            return _dbContext.Checkpoints.First(c => c.Id == Id);

                
        }

        public List<Checkpoint> GetMostPopularDestinations(int count = 4)
        {
            try
            {

                var popularDestinations = _dbContext.Checkpoints
                    .GroupBy(ch => new
                    {
                        City = ch.Location.City,
                        Country = ch.Location.Country,
                        ch.TourId
                    })
                    .OrderByDescending(g => g.Count())
                    .Take(count)
                    .Select(g => g.OrderByDescending(ch => ch.Name).First())
                    .ToList();

                return popularDestinations;
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching most popular destinations", e);
            }
        }
    }
}
