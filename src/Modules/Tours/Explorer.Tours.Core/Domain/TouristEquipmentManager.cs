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
    public Collection<Equipment> EquipmentList { get; private set; }
    public Collection<Equipment> PersonalCollection { get; private set; }

    public TouristEquipmentManager(int touristId, Collection<Equipment> equipmentList, Collection<Equipment> personalCollection)
    {
        TouristId = touristId;
        EquipmentList = equipmentList;
        PersonalCollection = personalCollection;
    }

    private void Validate()
    {
        if (TouristId <= 0) throw new Exception("TouristId must be a positive integer.");
        if (EquipmentList == null) throw new Exception("The equipment list must be initialized.");
        if (PersonalCollection == null) throw new Exception("Personal collection must be initialized.");
        foreach(var equipment in PersonalCollection)
        {
            if(!EquipmentList.Contains(equipment)) throw new Exception($"Equipment '{equipment.Name}' in personal collection is not available in the equipment list.");
        }
    }
}
