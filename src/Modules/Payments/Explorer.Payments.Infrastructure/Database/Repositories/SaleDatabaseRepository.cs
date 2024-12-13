using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    internal class SaleDatabaseRepository : ISaleRepository
    {
        private readonly PaymentsContext _dbContext;

        public SaleDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Sale Create(Sale sale)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Sale? Get(long id)
        {
            throw new NotImplementedException();
        }

        public Sale Update(Sale sale)
        {
            throw new NotImplementedException();
        }
    }
}
