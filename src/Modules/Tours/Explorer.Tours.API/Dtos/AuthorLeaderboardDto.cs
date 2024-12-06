using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class AuthorLeaderboardDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Ranking { get; set; }


    }
}
