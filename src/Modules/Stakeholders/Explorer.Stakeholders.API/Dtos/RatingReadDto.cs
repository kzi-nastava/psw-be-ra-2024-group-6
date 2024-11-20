using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class RatingReadDto
    {
        public long Id { get; set; }
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long? ImageId { get; set; }
        public string? ImageName {  get; set; }
        public string? ImageData {  get; set; }
        public int StarRating { get; set; }
        public string Comment { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
