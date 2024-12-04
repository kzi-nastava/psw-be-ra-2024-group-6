using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class WalletService : CrudService<WalletDto, Wallet>, IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper mapper;

        public WalletService(ICrudRepository<Wallet> crudRepository, IWalletRepository walletRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            this._walletRepository = walletRepository;
            this.mapper = mapper;
        }
        public Result<WalletDto> GetByUserId(long userId)
        {
            return MapToDto(_walletRepository.GetByUserId(userId));
        }
        public new Result<WalletDto> Update(WalletDto wallet)
        {
            return MapToDto(_walletRepository.Update(MapToDomain(wallet)));
        }
    }
}
