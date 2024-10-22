﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class ReviewService : CrudService<ReviewDto, Review>, IReviewService
    {
        public ReviewService(ICrudRepository<Review> repository, IMapper mapper) : base(repository, mapper) { }

    }
}
