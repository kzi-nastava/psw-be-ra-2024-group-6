using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum NotificationType
    {
        Tour,
        Blog,
        TourIssue,
        None
    }
    public class Notification : Entity
    {
        public string Content { get; private set; }
        public NotificationType Type { get; private set; }
        public long? LinkId { get; private set; }
        public long ReceiverId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsRead { get; private set; }

        public Notification() { }

        public Notification(string content, NotificationType type, long? linkId, long receiverId)
        {
            Content = content;
            Type = type;
            LinkId = linkId;
            ReceiverId = receiverId;
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Content)) throw new ArgumentException("Content cannot be empty");
            if (ReceiverId == 0) throw new ArgumentException("ReceiverId is required");
            if (Content.Length > 280) throw new ArgumentException("Message exceeds the maximum length of 280 char");
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }

    }
}
