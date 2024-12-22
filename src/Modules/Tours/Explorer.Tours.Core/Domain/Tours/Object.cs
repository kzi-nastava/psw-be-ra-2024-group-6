using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.Domain.Tours;

public class Object : Entity
{
    public string Name { get; private set; }
    public string ImageData { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; private set; }
    public long TourId { get; private set; }
    public Location Location { get; set; }

    public Object() { }
    public Object(string? name, string? imageData, string? description, Category category, long locationId, long tourId,Location location)
    {
        Name = name;
        ImageData = imageData;
        Description = description;
        Category = category;
        TourId = tourId;
        Location = location;
        Validate();
    }
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(ImageData)) throw new ArgumentException("Invalid ImageData");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
    }

    public void Update(Object obj)
    {
        this.Name = obj.Name;
        this.ImageData = obj.ImageData;
        this.Description = obj.Description;
        this.Category = obj.Category;
        this.Location = obj.Location;
    }
}
public enum Category
{
    WC, Restaurant, Parking, Other
}
