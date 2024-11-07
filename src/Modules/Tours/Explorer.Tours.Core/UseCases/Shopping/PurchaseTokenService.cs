using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class PurchaseTokenService : CrudService<PurchaseTokenDto,PurchaseToken>, IPurchaseTokenService
    {
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;
        private readonly ICrudRepository<PurchaseToken> _crudRepository;
        private readonly IMapper mapper;

        public PurchaseTokenService(ICrudRepository<PurchaseToken> crudRepository, IPurchaseTokenRepository purchaseTokenRepository,IMapper mapper) : base(crudRepository,mapper)
        {
            _crudRepository = crudRepository;
            _purchaseTokenRepository = purchaseTokenRepository;
            this.mapper = mapper;
        }
        public Result<List<PurchaseTokenDto>> GetByUserId(long userId)
        {
            return MapToDto(_purchaseTokenRepository.GetByUserId(userId));
        }

        public new Result<PurchaseTokenDto> Update(PurchaseTokenDto purchaseToken)
        {
            var result = MapToDto(_purchaseTokenRepository.Update(MapToDomain(purchaseToken)));
            return result;
        }
        public Result<PurchaseTokenDto> GetByUserAndTour(long userId,long tourId)
        {
            return MapToDto(_purchaseTokenRepository.GetByUserAndTour(userId, tourId));
        }
    }
}
