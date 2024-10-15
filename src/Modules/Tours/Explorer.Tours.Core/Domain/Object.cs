using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.Domain;

public class Object : Entity
{
    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; private set; }
    public long LocationId {  get; private set; } 
    public long TourId { get; private set; }
}

public enum Category
{
    WC, Restaurant, Parking, Other
}
