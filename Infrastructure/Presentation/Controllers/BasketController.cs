using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTO.BasketModule;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPut]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(result);
        }
    }
}
