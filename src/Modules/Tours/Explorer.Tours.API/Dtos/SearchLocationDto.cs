﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class SearchLocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double MaxDistance { get; set; }
    }
}
