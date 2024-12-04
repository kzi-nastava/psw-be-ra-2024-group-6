using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterExecutionRepository
    {
        public EncounterExecution Create(EncounterExecution encounter);

        public EncounterExecution Update(EncounterExecution encounter);
        public void Delete(long id);
        public EncounterExecution GetEncounterExecutionByTouristId(int id);
        public List<EncounterExecution> GetPagedEncounterExecutions();
        public EncounterExecution GetById(long id);
        public SocialEncounterExecution? GetStartedSocialByEncounterId(long encounterId);
        public SocialEncounterExecution? GetStartedSocialEncounterById(long id);
    }
}
