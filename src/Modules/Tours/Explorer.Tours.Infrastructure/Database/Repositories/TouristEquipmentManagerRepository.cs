using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class TouristEquipmentManagerRepository : ITouristEquipmentManagerRepository
{
    private readonly ToursContext _context;

    public TouristEquipmentManagerRepository(ToursContext context)
    {
        _context = context;
    }

    public List<TouristEquipmentManager> GetAllByTouristId(int touristId)
    {
        return _context.TouristEquipmentManagers
                       .Where(t => t.TouristId == touristId)
                       .ToList();
    }
    public TouristEquipmentManager Create(TouristEquipmentManager equipment)
    {
        _context.TouristEquipmentManagers.Add(equipment);
        _context.SaveChanges();
        return equipment;
    }
    public bool Delete(int touristId, int equipmentId)
    {
        var equipment = _context.TouristEquipmentManagers
                                .FirstOrDefault(t => t.TouristId == touristId && t.EquipmentId == equipmentId);

        if (equipment == null)
        {
            return false;
        }

        _context.TouristEquipmentManagers.Remove(equipment);
        _context.SaveChanges();

        return true;
    }
}
