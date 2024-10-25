using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IReviewService
    {
        public Result<ReviewDto> Create(ReviewDto reviewDto);
        IEnumerable<ReviewDto> GetAllReviews(); 

    }
}
