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
        public long BlogId { get; private set; }
        public Blog Blog { get; private set; }

        public BlogPicture() { }
        public BlogPicture(string url, long blogId, Blog blog = null)
        {
            Url = url;
            BlogId = blogId;
            Blog = blog;
        }
    }
}
