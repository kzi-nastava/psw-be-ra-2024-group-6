using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemWithMessageDto
    {
        public ProblemDto Problem { get; set; }
        public ProblemMessageDto Message { get; set; }

    }
}
