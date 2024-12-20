using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Persons
{
    public class Follower : ValueObject
    {

        public int PersonId { get;  }



        [JsonConstructor] 
        public Follower(int personId) {
            PersonId = personId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PersonId;
        }
    }
}
