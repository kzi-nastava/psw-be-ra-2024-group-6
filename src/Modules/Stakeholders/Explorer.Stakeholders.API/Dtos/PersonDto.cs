using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }
        public string Motto { get; set; }
        public long? ImageId { get; set; }
        public string? ImageName {  get; set; }
        public string? ImageData { get; set; }



    }
}
