using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class InternalPurchaseTokenService : CrudService<PurchaseTokenDto, PurchaseToken>, IInternalPurchaseTokenService
    {
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;
        private readonly ICrudRepository<PurchaseToken> _crudRepository;
        private readonly IMapper mapper;

        public InternalPurchaseTokenService(ICrudRepository<PurchaseToken> crudRepository, IPurchaseTokenRepository purchaseTokenRepository, IMapper mapper) : base(crudRepository, mapper)
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
        public Result<PurchaseTokenDto> GetByUserAndTour(long userId, long tourId)
        {
            return MapToDto(_purchaseTokenRepository.GetByUserAndTour(userId, tourId));
        }
    }
}
