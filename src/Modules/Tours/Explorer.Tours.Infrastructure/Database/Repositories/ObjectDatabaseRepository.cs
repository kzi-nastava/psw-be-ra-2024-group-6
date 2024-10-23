using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class ObjectDatabaseRepository : IObjectRepository
{
    private readonly ToursContext _dbContext;

    public ObjectDatabaseRepository(ToursContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Core.Domain.Object> GetAllByTourId(long tourId)
    {
        try
        {
            var ret = _dbContext.Objects.Where(o => o.TourId == tourId).ToList();
            return ret;
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }
}
