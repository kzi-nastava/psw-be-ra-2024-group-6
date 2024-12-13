using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {
        public Encounter Create(Encounter encounter);

        public Encounter Update(Encounter encounter);
        public void Delete (long id);
        public Encounter GetEncounter(long id);

        public List<Encounter> GetPagedEncounters();


        public List<Encounter> GetAllActiveEncounters();
        public List<Encounter> GetAllActiveEncountersForTourist(int touristId);

        public Encounter GetById(long id);
        public SocialEncounter GetSocialEncounterById(long id);
    }
}
