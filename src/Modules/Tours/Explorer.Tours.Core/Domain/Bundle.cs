using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
   
    public class Bundle : Entity
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public Status Status { get; private set; } = Status.Draft;
        public List<int> TourIds { get; private set; }
        public long AuthorId { get; private set; }


        private Bundle() { }
        public Bundle(string? name,double price,List<int> tours,int author)
        {
            
            Name = name;
            Price = price;
            TourIds = tours;
            AuthorId = author;

        }

        private bool CanArchive()
        {
            return Status == Status.Published;
        }

        private bool CanPublish()
        {
            return Status != Status.Published;

        }

        public bool Archive()
        {
            if (!CanArchive())
                return false;
            Status = Status.Archived;
            return true;
        }

        public bool Publish()
        {
            if (!CanPublish())
                return false;

            Status = Status.Published;
            return true;

        }


    }
}
