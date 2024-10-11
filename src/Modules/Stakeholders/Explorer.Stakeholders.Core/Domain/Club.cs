using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }

        public long OwnerId { get; private set; }

        public List<long> MemberIds {get; private set; }

        public Club(string name, string description, string imageUrl, long ownerId, List<long> memberIds)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            OwnerId = ownerId;
            MemberIds = memberIds;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        }
    }
}
