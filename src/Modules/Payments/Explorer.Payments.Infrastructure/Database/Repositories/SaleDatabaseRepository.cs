using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    internal class SaleDatabaseRepository
    {
        private readonly PaymentsContext _dbContext;

        public SaleDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
