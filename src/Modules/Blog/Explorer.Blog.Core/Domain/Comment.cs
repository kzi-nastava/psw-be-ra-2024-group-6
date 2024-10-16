using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Comment : Entity
    {
        public long Id { get; private set; }

        public string Text { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime UpdateDate { get; private set; }

        public int UserId { get; private set; }
        public int BlogId { get; private set; }

        public Comment( long id, string text, DateTime creationDate, DateTime updateDate, int userId, int blogId)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("Must contain text");
            Id = id;
            Text = text;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            UserId = userId;
            BlogId = blogId;
        }
    }
}
