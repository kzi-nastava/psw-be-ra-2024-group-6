using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Problems
{
    public class ProblemMessage : ValueObject
    {
        public string Content { get; private set; }
        public long SenderId { get; private set; }
        public DateTime CreationDate { get; private set; }

        public ProblemMessage()
        {

        }

        [JsonConstructor]
        public ProblemMessage( string content, long senderId, DateTime creationDate)
        {
            this.Content = content;
            this.SenderId = senderId;
            this.CreationDate = creationDate;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Content)) throw new ArgumentException("Invalid Content.");

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
