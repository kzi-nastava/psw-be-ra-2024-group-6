using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain;

public class TouristEquipmentManager : Entity
{
    public int TouristId { get; private set; }
    public int EquipmentId {  get; private set; }

    public TouristEquipmentManager(int touristId, int equipmentId)
    {
        TouristId = touristId;
        EquipmentId = equipmentId;
    }

    private void Validate()
    {
        if (TouristId <= 0) throw new Exception("TouristId must be a positive integer.");
        if (EquipmentId <= 0) throw new Exception("EquipmentId must be a positive integer.");
    }
}
