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
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IMapper mapper;

        public ShoppingCartService(ICrudRepository<ShoppingCart> crudRepository,
            IShoppingCartRepository shoppingCartRepository, IPurchaseTokenRepository purchaseTokenRepository, ITourRepository tourRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _purchaseTokenRepository = purchaseTokenRepository;
            _tourRepository = tourRepository;
            this.mapper = mapper;
        }



        public Result<ShoppingCartDto> GetByUserId(long userId)
        {
            return MapToDto(_shoppingCartRepository.GetByUserId(userId));
        }

        public Result<ShoppingCartDto> AddItem(long userId, long tourId)
        {
            var sc = _shoppingCartRepository.GetByUserId(userId);

            if (sc == null)
            {
                sc = new ShoppingCart(userId, new Price(0));
                _shoppingCartRepository.Create(sc);
            }

            var tour = _tourRepository.Get(tourId);
            if (tour == null)
            {
                return null;
            }

            sc.AddItem(tourId, tour.Name, tour.Cost);
            _shoppingCartRepository.Update(sc);
            return MapToDto(mapper.Map<ShoppingCart>(sc));
        }



        public Result<ShoppingCartDto> RemoveItem(int userId, int itemId)
        {

            var sc = _shoppingCartRepository.GetByUserId(userId);
            if (sc == null)
            {
                return Result.Fail<ShoppingCartDto>("Shopping cart not found.");
            }

            sc.RemoveItem(itemId);
            _shoppingCartRepository.Update(sc);
            return MapToDto(mapper.Map<ShoppingCart>(sc));
        }

        public Result<List<PurchaseTokenDto>> Checkout(long userId)
        {
            var sc = _shoppingCartRepository.GetByUserId(userId);
            if (sc == null)
            {
                return Result.Fail<List<PurchaseTokenDto>>("Shopping cart not found.");
            }

            var tokens = sc.Checkout();
            if(tokens.Count == 0)
            {
                return Result.Fail<List<PurchaseTokenDto>>("No items in the cart to checkout.");
            }

            foreach (var token in tokens)
            {
                _purchaseTokenRepository.Create(token);
            }

            _shoppingCartRepository.Update(sc);

            return Result.Ok();
        }

    }
}
