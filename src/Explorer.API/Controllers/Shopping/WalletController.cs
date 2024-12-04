using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Shopping
{
    [Authorize(Policy = "authorOrAdministratorOrTouristPolicy")]
    [Route("api/shopping/wallet")]
    public class WalletController : BaseApiController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("user/{userId:int}")]
        public ActionResult<WalletDto> GetByUserId(int userId)
        {
            var result = _walletService.GetByUserId(userId);
            return CreateResponse(result);
        }

        [HttpPut("{senderId:int}")]
        public ActionResult<WalletDto> Update(int senderId, [FromBody] WalletDto walletDto)
        {
            var result = _walletService.Update(walletDto,senderId);
            return CreateResponse(result);
        }
        [HttpGet("users")]
        public ActionResult<WalletDto> GetPaged()
        {
            var result = _walletService.GetPaged();
            return Ok(result);
        }
    }
}
