using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public long ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        public long? LinkId { get; set; }
        public bool IsRead { get; set; }
    }
}
