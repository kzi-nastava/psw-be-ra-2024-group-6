using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITouristEquipmentManagerRepository
{
    List<TouristEquipmentManager> GetAllByTouristId(int touristId);
    TouristEquipmentManager Create(TouristEquipmentManager equipment);
    void Delete(int touristId, int equipmentId);
}
