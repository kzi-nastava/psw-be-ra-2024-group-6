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
    public long? TourId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageData { get; private set; }
    public  Location Location { get; private set; }
    public string Secret { get; private set; }
    public List<long> EncounterIds { get; private set; }

    public PublicCheckpointRequest? PublicRequest { get; set; }

    public bool IsPublic { get; private set; }



    public Checkpoint() { }
    public Checkpoint(string? name, string? description, string? imageData, long? tourId,Location location, string secret, PublicCheckpointRequest? publicRequest = null)
    {
        Name = name;
        Description = description;
        ImageData = imageData;
        Location = location;
        TourId = tourId;
        Secret = secret;
        EncounterIds = new List<long>();
        PublicRequest = publicRequest;
       // IsPublic = isPublic;
        UpdateIsPublic();
        Validate();
       
    }
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (string.IsNullOrWhiteSpace(ImageData)) throw new ArgumentException("Invalid image data");

    }


    private void UpdateIsPublic()
    {
        IsPublic = PublicRequest != null && PublicRequest.Status == PublicCheckpointStatus.Approved;
    }

    public void ApprovePublicRequest()
    {
        if (PublicRequest != null)
        {
            PublicRequest.Approve();
            UpdateIsPublic();

        }
    }

    public void RejectPublicRequest(string comment)
    {
        if (PublicRequest != null)
        {
            PublicRequest.Reject(comment);
            UpdateIsPublic();
        }
    }

    public void Update(Checkpoint checkpoint)
    {
        this.Name = checkpoint.Name;
        this.Description = checkpoint.Description;
        this.ImageData = checkpoint.ImageData;
        this.Location = checkpoint.Location;
        this.PublicRequest = checkpoint.PublicRequest;
    }

    public double GetCheckpointDistance(double longitude, double latitude)
    {
        return Location.CalculateDistance(longitude, latitude);

    }


}

