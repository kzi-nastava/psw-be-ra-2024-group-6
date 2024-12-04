using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public long Id { get; set; }
        public long UserId {  get; set; }


        public string Name { get; set; }

        public string Surname { get; set; }

        public string AuthorImage { get; set; }


        public string Comment {  get; set; }

        public double Rating {  get; set; }
        public List<string> Images { get; set; }


        public DateTime ReviewDate { get; set; }

    }

}
