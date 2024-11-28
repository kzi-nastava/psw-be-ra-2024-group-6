using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
