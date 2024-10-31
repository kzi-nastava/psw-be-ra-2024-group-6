using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ProblemRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Problem Create(Problem entity)
        {
            var p = _dbContext.Problems.Add(entity).Entity;
            _dbContext.SaveChanges();
            return p;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _dbContext.Problems.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Problem Get(long id)
        {
            var p = _dbContext.Problems.Where(t => t.Id == id)
               .Include(t => t.Messages!).FirstOrDefault();
            if (p == null)
            {
                throw new KeyNotFoundException($"Problem not found: {id}");
            }
            return p;
        }

        public List<Problem> GetAll()
        {
            throw new NotImplementedException();
        }

        public PagedResult<Problem> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Problem Update(Problem entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }


    }
}
