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
    public long TourId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public  Location Location { get; private set; }

    public string Secret {  get; private set; }

    public Checkpoint() { }
    public Checkpoint(string? name, string? description, string? imageUrl, long tourId,Location location, string secret)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Location = location;
        TourId = tourId;
        Secret = secret;
        Validate();
        Secret = secret;
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
        this.Location = checkpoint.Location;
    }
}

