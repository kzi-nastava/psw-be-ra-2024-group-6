using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterDatabaseRepository : IEncounterRepository
    {

        private readonly EncountersContext _dbContext;

        public EncounterDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Encounter Create(Encounter encounter)
        {
            var en = _dbContext.Encounters.Add(encounter).Entity;
            _dbContext.SaveChanges();
            return en;
        }

        public Encounter Update(Encounter encounter)
        {
            try
            {
                _dbContext.Entry(encounter).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return encounter;
        }

        public void Delete(long id)
        {
            var entity = _dbContext.Encounters.FirstOrDefault(t => t.Id == id);
            _dbContext.Encounters.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Encounter GetEncounter(long Id)
        {
            return _dbContext.Encounters.First(e => e.Id == Id);
        }

        public List<Encounter> GetPagedEncounters()
        {
            
            return  _dbContext.Encounters.ToList();
            
        }

        public List<Encounter> GetAllActiveEncounters()
        {
            return _dbContext.Encounters.Where(e => e.Status == Status.Active).ToList();
        }

        public List<Encounter> GetAllActiveEncountersForTourist(int touristId)
        {

            return _dbContext.Encounters.Where(e => e.Status == Status.Active && e.CreatorId != touristId).ToList();

        }

        public Encounter GetById(long id)
        {
            return _dbContext.Encounters.First(e => e.Id == id);
        }

        public SocialEncounter GetSocialEncounterById(long id)
        {
            return _dbContext.Encounters.OfType<SocialEncounter>().FirstOrDefault(enc => enc.Id == id);
        }
    }
}
