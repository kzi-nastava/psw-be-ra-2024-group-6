using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IImageService
    {
        Result<ImageDto> Create(ImageDto image);
        Result<ImageDto> Get(long id);
        Result Delete(long id);
    }
}
