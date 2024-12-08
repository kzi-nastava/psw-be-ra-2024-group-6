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
    public interface IWalletService
    {
        public Result<WalletDto> Update(WalletDto wallet, long senderId);
        public Result<WalletDto> GetByUserId(long userId);
        public PagedResult<WalletDto> GetPaged();
    }
}
