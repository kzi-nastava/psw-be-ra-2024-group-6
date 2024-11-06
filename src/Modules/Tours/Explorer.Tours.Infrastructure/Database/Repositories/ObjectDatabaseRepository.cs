using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
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

    public List<Core.Domain.Tours.Object> GetAllByTourId(long tourId)
    {
        try
        {
            return _dbContext.Objects.Where(o => o.TourId == tourId).Include(o => o.Location).ToList();
            
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }
}
