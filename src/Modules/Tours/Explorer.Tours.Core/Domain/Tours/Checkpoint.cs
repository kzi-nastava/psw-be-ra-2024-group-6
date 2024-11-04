using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.Tours;

public class Checkpoint : Entity
{
    public long TourId { get; init; }
    public long LocationId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
    public virtual Location? Location { get; set; }
    public string Secret { get; init; }

    public Checkpoint() { }
    public Checkpoint(string name, string? description, string? imageUrl, long locationId, long tourId, string secret)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        LocationId = locationId;
        TourId = tourId;
        Secret = secret;
        Validate();
        
    }
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (string.IsNullOrWhiteSpace(ImageUrl)) throw new ArgumentException("Invalid ImageUrl");
    }

    public void Update(Checkpoint checkpoint)
    {
        this.Name = checkpoint.Name;
        this.Description = checkpoint.Description;
        this.ImageUrl = checkpoint.ImageUrl;
        this.LocationId = checkpoint.LocationId;
        Validate();
    }
}

