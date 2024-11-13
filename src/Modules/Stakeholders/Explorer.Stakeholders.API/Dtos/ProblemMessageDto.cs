using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemMessageDto
    {
        public string Content { get; set; }
        public long SenderId { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
