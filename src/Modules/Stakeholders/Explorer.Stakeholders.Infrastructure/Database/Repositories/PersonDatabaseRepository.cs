using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class PersonDatabaseRepository : IPersonRepository
    {
        private readonly StakeholdersContext _dbContext;

        public PersonDatabaseRepository(StakeholdersContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Person GetByUserId(int userId)
        {
            try 
            {
                var ret = _dbContext.People.FirstOrDefault(p => p.UserId == userId);
                _dbContext.SaveChanges();
                return ret;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }


        }

        public Person Update(Person person) 
        {

            try
            {
                _dbContext.People.Update(person);
                _dbContext.SaveChanges();
                return person;
            }
            catch (DbUpdateException ex) 
            {
                throw new KeyNotFoundException(ex.Message);

            }

        }

  
    }
}
