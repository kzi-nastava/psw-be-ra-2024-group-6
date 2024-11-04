using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowerCreateDto
    {
        public long FollowerId { get; set; } // ID korisnika koji želi da prati
        public string Name { get; set; } // Ime pratioca
        public string Surname { get; set; } // Prezime pratioca

        public FollowerCreateDto() { }
    }
}
