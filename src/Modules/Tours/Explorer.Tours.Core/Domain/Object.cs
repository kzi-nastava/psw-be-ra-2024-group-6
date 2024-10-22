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
    public long LocationId { get; private set; }
    public long TourId { get; private set; }

    public Object(string name, string imageUrl, string description, Category category,long locationId, long tourId)
    {
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        Category = category;
        LocationId = locationId;
        TourId = tourId;
        Validate();
    }
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(ImageUrl)) throw new ArgumentException("Invalid ImageUrl");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if ((int)Category < 0 || (int)Category > 3) throw new ArgumentException("Invalid category");
    }
}
public enum Category
{
    WC, Restaurant, Parking, Other
}
