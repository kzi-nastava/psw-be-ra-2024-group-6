using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowerReadDto
    {

        public long UserId { get; set; }

        public string Name { get; init; }
        public string Surname { get; init; }


    }
}
