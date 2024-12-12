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
        public string Name { get; set; }
        public string Data { get; set; }

        public int BlogId { get; set; }

        
        public BlogPictureDto() { }
        public BlogPictureDto(string name, int blogId, string data)
        {
            Name = name;
            BlogId = blogId;
            Data = data;
        }
        
    }
}
