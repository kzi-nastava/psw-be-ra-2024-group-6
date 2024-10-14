using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Location :Entity
    {
        public long latitude { get; init; }
        public long longitude { get; init; }

    }
}
