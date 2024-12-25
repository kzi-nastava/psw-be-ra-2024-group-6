using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.UseCases.Shopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Tours.Core.UseCases
{
    public class BundleService : BaseService<BundleDto,Bundle> , IBundleService
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IInternalPurchaseTokenService _purchaseTokenRepository;
        private readonly IInternalWalletService _walletRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IPaymentRecordService _paymentRecordService;
        private readonly ICheckpointRepository _checkpoinRepository;
        private readonly IShoppingCartService _shoppingCartService;
        public BundleService(IBundleRepository bundleRepository,
            IInternalPurchaseTokenService purchaseTokenRepository
            ,IInternalWalletService  walletRepository,
            ITourRepository tourRepository,
            IPaymentRecordService paymentRecordService ,
            ICheckpointRepository checkpointRepository,
            IShoppingCartService shoppingCartService,

            IMapper mapper) : base( mapper)
        {
            _bundleRepository = bundleRepository;
            _purchaseTokenRepository = purchaseTokenRepository;
            _walletRepository = walletRepository;
            _tourRepository = tourRepository;
            _paymentRecordService = paymentRecordService;
            _checkpoinRepository = checkpointRepository;
            
           
        }

        public Result<BundleDto> Buy(BundleDto bundle,int userId)
        {
            try
            {
                WalletDto wallet = _walletRepository.GetByUserId(userId).Value;
                List<PurchaseTokenDto> tokens = _purchaseTokenRepository.GetByUserId(userId).Value;
                int count = 0;
                if(wallet.AdventureCoins < bundle.Price)
                {
                    return Result.Fail(FailureCode.Forbidden).WithError("Not enough coins.");
                }


            


                foreach (int tour in bundle.TourIds)
                {

                    Tour tourM = _tourRepository.Get(tour);
                    bool exists = false;
                    
                    PurchaseTokenDto dto = new PurchaseTokenDto
                    {
                        UserId = userId,
                        TourId = tour,
                        PurchaseDate = DateTime.UtcNow,
                        isExpired = false

                    };

                    foreach (PurchaseTokenDto token in tokens)
                    {
                        if (token.TourId == tour)
                        {
                            exists = true;
                            break;
                        }
                        
                      
                        
                    }

                    if(!exists)
                    { 
                        _purchaseTokenRepository.Create(dto);
                        count++;
                    }
                    
                }

                if (count == 0)
                {
                    return Result.Fail(FailureCode.Conflict).WithError("You already have all of these tours.");
                }

                PriceDto price = new PriceDto
                {
                    Amount = bundle.Price
                };

                PaymentRecordDto record = new PaymentRecordDto
                {
                    TouristId = userId,
                    ResourceId = (long)bundle.Id,
                    ResourceTypeId = 2,
                    Price = bundle.Price,
                    PaymentDate = DateTime.UtcNow,

                };
                
                _paymentRecordService.Create(record);


                wallet.AdventureCoins -= (long)bundle.Price;
                _walletRepository.Update(wallet);
                return bundle;
            }
            catch (ArgumentException ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");

            }
        }
        public Result<List<BundleDto>> GetAll()
        {
            try
            {
                var bundles = _bundleRepository.GetAll();
                return MapToDto(bundles);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BundleDto> Update(BundleDto bundle)
        {
            try
            {

                Bundle bundle_m = _bundleRepository.Update(MapToDomain(bundle));
                return MapToDto(bundle_m);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("bundle not found");
            }
        }

        public Result<BundleDto> Publish(long bundleId)
        {
            try
            {
                Bundle bundle = _bundleRepository.GetById(bundleId);
                if (!bundle.Publish())
                    return Result.Fail("publish failed");
                _bundleRepository.Update(bundle);
                return MapToDto(bundle);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("bundle not found");
            }


        }

        public Result<BundleDto> Archive(long bundleId)
        {
            try
            {
                Bundle bundle = _bundleRepository.GetById(bundleId);
                if (!bundle.Archive())
                    return Result.Fail("archiving failed");
                _bundleRepository.Update(bundle);
                return MapToDto(bundle);

            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail("bundle not found");
            }


        }

        public Result<List<BundleDto>> GetAllByUserId(long userId)
        {
            try
            {
                var bundles = _bundleRepository.GetAll();
                List<Bundle> bundles_filtered = new List<Bundle>();
                foreach (var bundle in bundles)
                {
                    if(bundle.AuthorId == userId)
                        bundles_filtered.Add(bundle);
                }

                return MapToDto(bundles_filtered);
            }
            catch(Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BundleDto> Create(BundleDto bundle)
        {
            try
            {
                List <Tour> tours = new List<Tour>();
                foreach(int x in bundle.TourIds)
                {
                    tours.Add(_tourRepository.GetById(x));
                }
                
                
                List<string> images = new List<string>();
                foreach (var tour in tours)
                {
                    List<Checkpoint> checkpoints = _checkpoinRepository.GetByTourId(tour.Id);
                    images.Add(checkpoints[0].ImageData);
                }

                bundle.ImageData = CreateCollage(images);
                var result = _bundleRepository.Create(MapToDomain(bundle));
                return MapToDto(result);
            }
            catch(ArgumentException ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");

            }
        }

        public string CreateCollage(List<string> base64Images)
        {
            if (base64Images == null || base64Images.Count == 0)
                throw new ArgumentException("At least one image is required.");

            // Limit to first 4 images if there are more than 4
            base64Images = base64Images.Take(4).ToList();

            // Decode base64 strings into images
            var images = base64Images.Select(base64 =>
            {
                // Remove the data URI scheme prefix, if present
                if (base64.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
                {
                    var base64Data = base64.Substring(base64.IndexOf(",") + 1);
                    base64 = base64Data;
                }

                try
                {
                    var bytes = Convert.FromBase64String(base64);
                    using var ms = new MemoryStream(bytes);
                    return System.Drawing.Image.FromStream(ms);
                }
                catch (Exception)
                {
                    throw new ArgumentException("One or more provided images are invalid base64 strings.");
                }
            }).ToList();

            // Determine collage dimensions based on the number of images
            int collageWidth, collageHeight;

            switch (images.Count)
            {
                case 1: // Only one image, return it as is
                    return base64Images[0];

                case 2: // Two images side by side
                    collageWidth = images[0].Width + images[1].Width;
                    collageHeight = Math.Max(images[0].Height, images[1].Height);
                    break;

                case 3: // Three images side by side
                    collageWidth = images.Sum(img => img.Width);
                    collageHeight = images.Max(img => img.Height);
                    break;

                case 4: // Four images in a 2x2 grid
                    collageWidth = Math.Max(images[0].Width, images[1].Width) + Math.Max(images[2].Width, images[3].Width);
                    collageHeight = Math.Max(images[0].Height, images[2].Height) + Math.Max(images[1].Height, images[3].Height);
                    break;

                default:
                    throw new InvalidOperationException("Unexpected image count.");
            }

            // Create the collage bitmap
            using var collage = new Bitmap(collageWidth, collageHeight);
            using var graphics = Graphics.FromImage(collage);
            graphics.Clear(Color.White); // Optional: Set a background color

            // Draw each image in its respective position
            int xOffset = 0, yOffset = 0;

            switch (images.Count)
            {
                case 2: // Side by side
                    graphics.DrawImage(images[0], 0, 0);
                    graphics.DrawImage(images[1], images[0].Width, 0);
                    break;

                case 3: // Side by side
                    foreach (var image in images)
                    {
                        graphics.DrawImage(image, xOffset, 0);
                        xOffset += image.Width;
                    }
                    break;

                case 4: // 2x2 grid
                    graphics.DrawImage(images[0], 0, 0);
                    graphics.DrawImage(images[1], images[0].Width, 0);
                    graphics.DrawImage(images[2], 0, images[0].Height);
                    graphics.DrawImage(images[3], images[2].Width, images[0].Height);
                    break;
            }

            // Convert the resulting collage back to base64
            using var ms = new MemoryStream();
            collage.Save(ms, ImageFormat.Png);
            return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
        }

        public List<long> GetBundleIdsByTourId(long tourId)
        {
            return _bundleRepository.GetByTourId(tourId).Select(b => b.Id).ToList();
        }
    }
}
