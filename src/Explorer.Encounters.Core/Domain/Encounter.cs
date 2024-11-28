using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{

    public enum Status
    {
        Active,
        Draft,
        Archived
    }

    public enum TypeEncounter { 
        Social,
        Location,
        Misc
    }

    public class Encounter : Entity
    {
        public string Name {  get; private set; }
        public string Description { get; private set; }

        public Location Location { get; private set; }

        public int Xp {  get; private set; }

        public Status Status { get; private set; }

        public TypeEncounter TypeEncounter { get; private set; }
        public int CreatorId { get; private set; }
        public bool IsRequiredForCheckpoint { get; private set; }

        public bool IsAccepted {  get; private set; }
        public Encounter()
        {

        }
             

        public Encounter(string name,string description,Location loc,int xp,Status status,TypeEncounter type, int creatorId, bool isRequiredForCheckpoint = false)
        {
            Name = name;
            Description = description;
            Location = loc;
            Xp = xp;
            Status = status;
            TypeEncounter = type;
            CreatorId = creatorId;
            IsRequiredForCheckpoint = isRequiredForCheckpoint;
            IsAccepted = false;
            Validate();
        }

        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Description is required.");
        }

        public bool IsActive()
        {
            return Status == Status.Active;
        }


        public void SetCreatorId(int creatorId)
        {
            CreatorId = creatorId;
        }

        public void AcceptEncounter() 
        {
            IsAccepted = true;
            Status = Status.Active;
        }
    }
}
