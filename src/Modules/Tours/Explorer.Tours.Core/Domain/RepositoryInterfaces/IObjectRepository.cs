using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IObjectRepository
{
    List<Tours.Object> GetAllByTourId(long tourId);
}
