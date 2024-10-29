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
        private readonly ITourRepository _tourRepository;
        private readonly IMapper mapper;

        public ShoppingCartService(ICrudRepository<ShoppingCart> crudRepository,
            IShoppingCartRepository shoppingCartRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
        }



        public Result<ShoppingCartDto> GetByUserId(long userId)
        {
            return MapToDto(_shoppingCartRepository.GetByUserId(userId));
        }

        public Result<ShoppingCartDto> AddItem(long shoppingCartId, long tourId, long userId)
        {
            var sc = _shoppingCartRepository.Get(shoppingCartId);

            if (sc == null)
            {
                sc = new ShoppingCart(userId, 0);
            }

            var tour = _tourRepository.Get(tourId);
            if (tour == null)
            {
                return null;
            }

            sc.AddItem(tourId, tour.Name, tour.Cost);
            return MapToDto(mapper.Map<ShoppingCart>(sc));
        }



        public Result<ShoppingCartDto> RemoveItem(int shoppingCartId, int itemId)
        {
            var sc = _shoppingCartRepository.Get(shoppingCartId);
            if (sc == null)
            {
                return Result.Fail<ShoppingCartDto>("Shopping cart not found.");
            }

            sc.RemoveItem(itemId);
            return MapToDto(mapper.Map<ShoppingCart>(sc));
        }
    }
}
