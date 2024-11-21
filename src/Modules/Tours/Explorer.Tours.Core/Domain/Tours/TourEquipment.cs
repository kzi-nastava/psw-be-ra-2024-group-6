using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TourEquipment: Entity
    {
        public long TourId { get; set; }
        public long EquipmentId { get; set; }
        public Tour Tour { get; set; }
        public Equipment Equipment { get; set; }

        public TourEquipment() { }
        
        public TourEquipment(long tourId, long equipmentId, Tour tour, Equipment equipment)
        {
            TourId = tourId;
            EquipmentId = equipmentId;
            Tour = tour;
            Equipment = equipment;
        }
        public TourEquipment(long tourId, long equipmentId)
        {
            TourId = tourId;
            EquipmentId = equipmentId;
        }
    }
}
