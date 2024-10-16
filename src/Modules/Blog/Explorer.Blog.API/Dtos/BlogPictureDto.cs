using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogPictureDto
    {
        public int Id {  get; set; }
        public string Url { get; set; }
        public int BlogId { get; set; }

        
        public BlogPictureDto() { }
        public BlogPictureDto(string url, int blogId)
        {
            Url = url;
            BlogId = blogId;
        }
        
    }
}
