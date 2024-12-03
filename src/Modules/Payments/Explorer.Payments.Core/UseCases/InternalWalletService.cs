using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class InternalWalletService : CrudService<WalletDto, Wallet>, IInternalWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper mapper;

        public InternalWalletService(ICrudRepository<Wallet> crudRepository,IWalletRepository walletRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            this._walletRepository = walletRepository;
            this.mapper = mapper;
        }
        public Result<WalletDto> Create(long userId)
        {
            var wallet = new Wallet(userId,0);
            return MapToDto(_walletRepository.Create(wallet));
        }
    }
}
