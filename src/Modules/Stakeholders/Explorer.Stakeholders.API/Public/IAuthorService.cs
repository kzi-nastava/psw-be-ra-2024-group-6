using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAuthorService
    {
        public Result<List<PersonDto>> GetMostPopularAuthors();
    }
}
