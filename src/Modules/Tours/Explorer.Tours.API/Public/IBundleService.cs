using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IBundleService
    {
        Result<List<BundleDto>> GetAllByUserId(long userId);
        Result<BundleDto> Create(BundleDto bundle);
        Result<BundleDto> Publish(long bundleId);
        Result<BundleDto> Archive(long bundleId);
        Result<BundleDto> Update(BundleDto bundle);



    }
}
