using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterExecutionDatabaseRepository : IEncounterExecutionRepository
    {
        private readonly EncountersContext _dbContext;

        public EncounterExecutionDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
        }


        public EncounterExecution Create(EncounterExecution encounter)
        {
            var en = _dbContext.EncounterExecutions.Add(encounter).Entity;
            _dbContext.SaveChanges();
            return en;
        }

        public EncounterExecution Update(EncounterExecution encounter)
        {
            try
            {
                _dbContext.Entry(encounter).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return encounter;
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        public void Delete(long id)
        {
            var entity = _dbContext.EncounterExecutions.FirstOrDefault(t => t.Id == id);
            _dbContext.EncounterExecutions.Remove(entity);
            _dbContext.SaveChanges();
        }

        public EncounterExecution GetEncounterExecutionByTouristId(int id)
        {
            return _dbContext.EncounterExecutions.First(e => e.TouristId == id);
        }
        public List<EncounterExecution> GetPagedEncounterExecutions()
        {

            return _dbContext.EncounterExecutions.ToList();

        }

        public EncounterExecution GetEncounterExecution(long encounterId, int touristId)
        {

            return _dbContext.EncounterExecutions.First(e => e.EncounterId == encounterId && e.TouristId == touristId);
        }
        public EncounterExecution GetById(long id)
        {
            return _dbContext.EncounterExecutions.First(e => e.Id == id);
        }
    }
}
