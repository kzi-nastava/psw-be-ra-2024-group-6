using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.Tours;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public enum Status
{
    Draft,
    Published,
    Archived
}

public class Tour : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public List<string> Tags { get; private set; }
    public Price Price { get; private set; }
    public Status Status { get; private set; } = Status.Draft;
    public long AuthorId { get; private set; }
    public Distance TotalLenght { get; private set; }
    public DateTime? ArchieveTime { get; private set; }
    public List<TourDuration> Durations { get; private set; }

    public List<Checkpoint> Checkpoints { get; private set; }
    public List<Object> Objects { get; private set; }
    public List<Equipment> Equipment { get; private set; }
    public List<Review> Reviews { get; private set; }


    public Tour(string name, string description, Difficulty difficulty, List<string> tags, long authorId, Distance totalLenght, List<TourDuration> durations)
    {
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Tags = new List<string>(tags);
        AuthorId = authorId;
        TotalLenght = totalLenght;
        Durations = new List<TourDuration>(durations);
        Validate();
    }

    public Tour(string name, string description, Difficulty difficulty, List<string> tags, long authorId)
    {
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Tags = new List<string>(tags);
        AuthorId = authorId;
        Validate();
    }
    public Tour() { }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Description is required.");
        if (Tags == null || Tags.Count == 0) throw new ArgumentException("At least one tag is required.");

    }

    public void AddCheckpoint(Checkpoint checkpoint)
    {
        if (Checkpoints == null)
        {
            Checkpoints = new List<Checkpoint>();
        }
        Checkpoints.Add(checkpoint);
    }

    public void AddObject(Object obj)
    {
        if (Objects == null)
        {
            Objects = new List<Object>();
        }
        Objects.Add(obj);
    }

    public void AddEquipment(Equipment equipment)
    {
        if (Equipment == null)
        {
            Equipment = new List<Equipment>();
        }
        Equipment.Add(equipment);
    }

    public void RemoveCheckpoint(Checkpoint checkpoint)
    {
        if (Checkpoints == null)
        {
            return;
        }
        Checkpoints.Remove(checkpoint);
    }

    public void RemoveObject(Object obj)
    {
        if (Objects == null)
        {
            return;
        }
        Objects.Remove(obj);
    }

    public void RemoveEquipment(Equipment equipment)
    {
        if (Equipment == null)
        {
            return;
        }
        Equipment.Remove(equipment);
    }

    public void UpdateCheckpoint(Checkpoint checkpoint)
    {
        if (Checkpoints == null)
        {
            Checkpoints = new List<Checkpoint>();
        }
        var existingCheckpoint = Checkpoints.FirstOrDefault(c => c.Id == checkpoint.Id);
        if (existingCheckpoint == null)
        {
            return;
        }
        existingCheckpoint.Update(checkpoint);
    }

    public void UpdateObject(Object obj)
    {
        if (Objects == null)
        {
            Objects = new List<Object>();
        }
        var existingObject = Objects.FirstOrDefault(o => o.Id == obj.Id);
        if (existingObject == null)
        {
            return;
        }
        existingObject.Update(obj);
    }

    public void UpdateEquipment(Equipment equipment)
    {
        if (Equipment == null)
        {
            Equipment = new List<Equipment>();
        }
        var existingEquipment = Equipment.FirstOrDefault(e => e.Id == equipment.Id);
        if (existingEquipment == null)
        {
            return;
        }
        //existingEquipment.Update(equipment);
    }

    public void Archive()
    {

    }

    public void Publish()
    {

    }

    private bool CanPublish()
    {
        throw new NotImplementedException();
    }

    public List<Checkpoint> GetPreviewCheckpoints()
    {
        throw new NotImplementedException();
    }
    public double GetAverageRating()
    {
        throw new NotImplementedException();
    }

    public bool IsAuthorOwner(int userId)
    {
        return userId == AuthorId;
    }
}
