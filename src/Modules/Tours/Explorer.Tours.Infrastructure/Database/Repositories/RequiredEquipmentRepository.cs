using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class RequiredEquipmentRepository : IRequiredEquipmentRepository
    {
        private readonly ToursContext _context;

        public RequiredEquipmentRepository(ToursContext context)
        {
            _context = context;
        }
        public RequiredEquipment Create(RequiredEquipment requiredEquipment)
        {
            _context.RequiredEquipments.Add(requiredEquipment);
            _context.SaveChanges();
            return requiredEquipment;
        }

        public bool Delete(long id)
        {
            var requiredEquipment = _context.RequiredEquipments.Find(id);
            if (requiredEquipment == null) return false;
            _context.RequiredEquipments.Remove(requiredEquipment);
            _context.SaveChanges();
            return true;

        }

        public ICollection<RequiredEquipment> GetAllByTour(int tourId)
        {
            return _context.RequiredEquipments
                .Where(re => re.TourId == tourId)
                .ToList();
        }
    }
}
