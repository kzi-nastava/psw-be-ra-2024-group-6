using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Explorer.Stakeholders.Core.Domain.Persons;
using Microsoft.IdentityModel.Tokens;
using Explorer.Payments.Core.Domain;

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
                var existingPerson = _dbContext.People.Find(person.Id);
                if (existingPerson != null)
                {
                    _dbContext.Entry(existingPerson).State = EntityState.Detached;
                }
                _dbContext.People.Update(person);
                _dbContext.SaveChanges();
                return person;
            }
            catch (DbUpdateException ex) 
            {
                throw new KeyNotFoundException(ex.Message);

            }

        }

        public List<Follower> GetFollowers(Person person)
        {
            try
            {
                var originalPerson = _dbContext.People.FirstOrDefault(p => p.Id == person.Id);
                if(originalPerson != null)
                {
                    return originalPerson.Followers ?? new List<Follower>(); 
                }
                return new List<Follower>();
            }
            catch(Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }
        public List<Follower> GetFollowings(Person person)
        {
            try
            {
                var originalPerson = _dbContext.People.FirstOrDefault(p => p.Id == person.Id);
                if (originalPerson != null)
                {
                    return originalPerson.Followings ?? new List<Follower>();
                }
                return new List<Follower>();
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public List<Person> GetMostFollowedAuthors(List<long> authorsIds, int count = 4)
        {
            try
            {
                if (authorsIds == null || authorsIds.Count == 0)
                {
                    throw new ArgumentException("authorIds cannot be empty or null.");
                }

                var query = @"SELECT * 
                      FROM stakeholders.""People"" 
                      WHERE ""Id"" = ANY ({0}) 
                      ORDER BY jsonb_array_length(""Followers"") DESC 
                      LIMIT {1}";

                return _dbContext.People
                    .FromSqlRaw(query, authorsIds.ToArray(), count)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching most followed authors", e);
            }
        }
    }
}
