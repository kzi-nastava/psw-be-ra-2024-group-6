using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class PurchaseTokenService : BaseService<PurchaseTokenDto, PurchaseToken>, IPurchaseTokenService
    {
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;

        public PurchaseTokenService(IPurchaseTokenRepository tokenRepository, IMapper mapper) : base(mapper)
        {
            _purchaseTokenRepository = tokenRepository;
        }

        public Result<List<PurchaseTokenDto>> GetByUserId(long id)
        {
            return MapToDto(_purchaseTokenRepository.GetByUserId(id));
        }

        public Result<PurchaseTokenDto?> GetByUserAndTour(long userId, long tourId)
        {
            return MapToDto(_purchaseTokenRepository.GetByUserAndTour(userId, tourId));
        }
    }
}
