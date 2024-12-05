using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping;

public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IPurchaseTokenRepository _purchaseTokenRepository;
    private readonly IInternalTourPaymentService _tourPaymentService;
    private readonly IInternalUserPaymentService _internalUserPaymentService;
    private readonly IWalletRepository _walletRepository;
    private readonly IPaymentRecordRepository _paymentRecordRepository;
    private readonly IMapper mapper;

    public ShoppingCartService(ICrudRepository<ShoppingCart> crudRepository,

        IShoppingCartRepository shoppingCartRepository, IPurchaseTokenRepository purchaseTokenRepository, IInternalTourPaymentService tourPaymentService, IWalletRepository walletRepository, IInternalUserPaymentService userPaymentService, IPaymentRecordRepository paymentRecordRepository, IMapper mapper) : base(crudRepository, mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _purchaseTokenRepository = purchaseTokenRepository;
        _tourPaymentService = tourPaymentService;
        _walletRepository = walletRepository;
        _internalUserPaymentService = userPaymentService;
        _paymentRecordRepository = paymentRecordRepository;
        this.mapper = mapper;
    }



    public Result<ShoppingCartDto> GetByUserId(long userId)
    {
        return MapToDto(_shoppingCartRepository.GetByUserId(userId));
    }

    public Result<ShoppingCartDto> AddItem(long userId, long resourceId, long resourceTypeId)
    {
        var sc = _shoppingCartRepository.GetByUserId(userId);

        if (sc == null)
        {
            sc = new ShoppingCart(userId, new Price(0));
            _shoppingCartRepository.Create(sc);
        }

        // ako je tura
        if(resourceTypeId == 1)
        {
            var tour = _tourPaymentService.Get(resourceId);

            if (tour == null || !tour.IsPublished)
            {
                return MapToDto(mapper.Map<ShoppingCart>(sc));
            }
            // ovde jos treba proveriti da li je na akciji
            
            if(sc.AddTour(resourceId, tour.Name, tour.Price.Amount) == null)
            {
                return MapToDto(mapper.Map<ShoppingCart>(sc));
            }
        }
        // ako je bundle
        if(resourceTypeId == 2)
        {

        }

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

    public Result<CheckoutResultDto> Checkout(int userId)
    {
        var sc = _shoppingCartRepository.GetByUserId(userId);
        if (sc == null)
        {
            return Result.Fail<CheckoutResultDto>("Shopping cart not found.");
        }

        var tokens = sc.Checkout();
        if (tokens.Count == 0)
        {
            return Result.Fail<CheckoutResultDto>("No items in the cart to checkout.");
        }
      
        foreach (var token in tokens)
        {
            var existingToken = _purchaseTokenRepository.GetByUserAndTour(token.UserId, token.TourId);
            if (existingToken != null)
            {
                return Result.Fail<CheckoutResultDto>($"You already bought one of the tours in your cart");
            }
        }

        var wallet = _walletRepository.GetByUserId(userId);
        if(wallet == null)
        {
            return Result.Fail<CheckoutResultDto>("Wallet not found.");
        }

        if (wallet.AdventureCoins >= sc.TotalPrice.Amount)
        {
            double newPrice = wallet.AdventureCoins - sc.TotalPrice.Amount;
            var newWallet = new Wallet(userId, (long)newPrice);
            _walletRepository.Update(wallet);
        }

        foreach (var token in tokens)
        {
            _purchaseTokenRepository.Create(token);
            // save the payment history
            var tour = _tourPaymentService.Get(token.TourId);
            var tourPrice = new Price(tour.Price.Amount);
            _paymentRecordRepository.Create(new PaymentRecord(token.UserId, token.TourId, tourPrice, DateTime.UtcNow));
            // send notification
            SendNotification(token.TourId, userId, "You have successfully bought tour: " + token.TourId);
        }
        _shoppingCartRepository.Update(sc);


        var cartDto = MapToDto(sc);
        var tokensDto = tokens.Select(token => new PurchaseTokenDto
        {
            Id = (int)token.Id,
            UserId = (int)token.UserId,
            TourId = (int)token.TourId,
            PurchaseDate = token.PurchaseDate
        }).ToList();

        var resultDto = new CheckoutResultDto
        {
            ShoppingCart = cartDto,
            PurchaseTokens = tokensDto
        };

        return Result.Ok(resultDto);
    }
    
    private void SendNotification(long tourId, long receiverId, string content)
    {
        _internalUserPaymentService.SendNotification(content, receiverId, tourId, false);
    }
}