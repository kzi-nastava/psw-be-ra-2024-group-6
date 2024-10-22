using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Explorer.Tours.Core.Domain
{
    public class Tour : Entity
    {
        public string Name { get; private set; }
        public int AuthorId { get; private set; }

        public Tour(string name, int authorId)
        {
            Name = name;
            AuthorId = authorId;
        }
    }
}
