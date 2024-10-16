using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class RequiredEquipment : Entity
    {
        public long TourId { get; private set; }
        public long EquipmentId { get; private set; }

        public RequiredEquipment(long tourId, long equipmentId)
        {
            TourId = tourId;
            EquipmentId = equipmentId;
        }

        private void Validate()
        {
            if (TourId <= 0) throw new ArgumentException("Invalid TourId");
            if (EquipmentId <= 0) throw new ArgumentException("Invalid EquipmentId");
        }
    }
}
