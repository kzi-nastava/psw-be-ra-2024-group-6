using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
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
        public List<Checkpoint> GetByTourId(int tourId)
        {
            try
            {
                var ret = _dbContext.Checkpoint.Where(p => p.TourId == tourId).ToList();
                return ret;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }
    }
}
