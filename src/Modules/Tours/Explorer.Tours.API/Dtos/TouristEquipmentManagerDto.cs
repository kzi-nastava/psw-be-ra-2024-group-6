using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;

public class TouristEquipmentManagerDto
{
    public int TouristId { get; set; }
    public Collection<EquipmentDto> EquipmentList { get; set; }
    public Collection<EquipmentDto> PersonalCollection { get; set; }
}
