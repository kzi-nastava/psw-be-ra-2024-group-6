using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class CheckpointDatabaseRepository : ICheckpointRepository
    {
        private readonly ToursContext _dbContext;

        public CheckpointDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Checkpoint> GetByTourId(long tourId)
        {
                return _dbContext.Checkpoints
                    .Where(p => p.TourId == tourId)
                    .Include(c => c.Location)
                    .ToList();
        }
    }
}
