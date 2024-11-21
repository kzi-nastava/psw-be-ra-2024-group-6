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
    public class TourEquipmentRepository: ITourEquipmentRepository
    {
        private readonly ToursContext _context;

        public TourEquipmentRepository(ToursContext context)
        {
            _context = context;
        }

        public TourEquipment Create(TourEquipment tourEquipment)
        {
            var te = _context.TourEquipment.Add(tourEquipment).Entity;
            _context.SaveChanges();
            return te;
        }

    }
}
