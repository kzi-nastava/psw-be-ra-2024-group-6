using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class EquipmentRepository: IEquipmentRepository
    {
        private readonly ToursContext _context;

        public EquipmentRepository(ToursContext dbContext)
        {
            _context = dbContext;
        }
        public List<Equipment> GetByIds(List<long> ids)
        {
            return _context.Equipment.Where(e => ids.Contains(e.Id)).ToList();
        }
    }
}
