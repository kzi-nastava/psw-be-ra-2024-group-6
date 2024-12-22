using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ISaleRepository
    {
        public Sale Create(Sale sale);
        public Sale Update(Sale sale);
        public void Delete(long id);
        public Sale? Get(long id);
        public Sale GetByTourId(long tourId);
    }
}
