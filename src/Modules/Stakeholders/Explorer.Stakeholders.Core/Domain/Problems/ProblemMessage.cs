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
        public long ProblemId { get; set; }
        public string Content { get; set; }
        public long SenderId { get; set; }
        public DateTime DateTime { get; set; }

        [JsonConstructor]
        public ProblemMessage(long problemId, string content, long senderId, DateTime dateTime)
        {
            ProblemId = problemId;
            Content = content;
            SenderId = senderId;
            DateTime = dateTime;
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
