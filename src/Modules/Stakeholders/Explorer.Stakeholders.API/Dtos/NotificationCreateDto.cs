using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationCreateDto
    {
        public string Content { get; set; }
        public string Type { get; set; }
        public long? LinkId { get; set; }



    }
}
