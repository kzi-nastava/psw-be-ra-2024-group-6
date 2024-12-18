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
using Explorer.Stakeholders.API.Internal;

namespace Explorer.Payments.Core.UseCases
{
    public class WalletService : CrudService<WalletDto, Wallet>, IWalletService
    {
        private readonly ICrudRepository<Wallet> _crudRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly IMapper mapper;

        public WalletService(ICrudRepository<Wallet> crudRepository, IWalletRepository walletRepository, IInternalNotificationService internalNotificationService, IMapper mapper) : base(crudRepository, mapper)
        {
            this._internalNotificationService = internalNotificationService;
            this._walletRepository = walletRepository;
            this._crudRepository = crudRepository;
            this.mapper = mapper;
        }
        public Result<WalletDto> GetByUserId(long userId)
        {
            return MapToDto(_walletRepository.GetByUserId(userId));
        }
        public Result<WalletDto> Update(WalletDto wallet, long senderId)
        {
            double AdventureCoinsAwarded = wallet.AdventureCoins;
            Wallet oldWallet = _walletRepository.GetByUserId(wallet.UserId);
            if (oldWallet != null) 
            {
                AdventureCoinsAwarded = wallet.AdventureCoins - oldWallet.AdventureCoins;
            }
            _internalNotificationService.SendWalletNotification(wallet.UserId, senderId, AdventureCoinsAwarded);    //sends a notification to the user whose wallet was updated
            return MapToDto(_walletRepository.Update(MapToDomain(wallet)));
        }
        public PagedResult<WalletDto> GetPaged()
        {
            int page = 1;
            int pageSize = 10;
            var result = MapToDto(_crudRepository.GetPaged(page, pageSize));

            if (result.IsFailed)
            {
                throw new Exception("Failed to map");
            }

            return result.Value;
        }
    }
}
