using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogPicture : Entity
    {
        public string Url {  get; private set; }
        public int BlogId { get; private set; }

        public BlogPicture(string url, int blogId) {
            Url = url;
            BlogId = blogId;
        }
    }
}
