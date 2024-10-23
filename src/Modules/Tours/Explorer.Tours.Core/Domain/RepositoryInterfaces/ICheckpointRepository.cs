using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository
    {
        List<Checkpoint> GetByTourId(long tourId);
        Checkpoint Create(Checkpoint checkpoint);
    }
}
