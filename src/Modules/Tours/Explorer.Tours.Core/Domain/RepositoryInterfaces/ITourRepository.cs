using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetByUserId(long userId);
        public Tour Create(Tour tour);
        public Tour Update(Tour tour);
        public void Delete(long id);
        public Tour GetAggregate(long id);
        public Tour Get(long id);
        public List<Tour> GetPublishedToursWithCheckpoints();
    }
}
