using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IProblemRepository
    {
        public Problem Create(Problem problem);
        public Problem Update(Problem problem);
        public void Delete(long id);
        public Problem Get(long id);

    }
}
