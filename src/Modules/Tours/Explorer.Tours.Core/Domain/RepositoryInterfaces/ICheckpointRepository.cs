using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository
    {
        List<Checkpoint> GetByTourId(long tourId);
        Checkpoint Create(Checkpoint checkpoint);

        Checkpoint Get(long id);
        public List<Checkpoint> GetMostPopularDestinations(int count = 4);
        public List<int> GetTourIdsForDestination(string city, string country, int page, int pageSize);
    }
}
