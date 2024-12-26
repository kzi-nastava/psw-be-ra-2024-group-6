using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ISaleService
    {
        Result<SaleDto> Create(SaleDto sale);
        Result<SaleDto> Update(SaleDto sale);
        Result Delete(int id);
        Result<SaleDto> Get(int id);
        Result<PagedResult<SaleDto>> GetPaged(int page, int pageSize);
    }
}
