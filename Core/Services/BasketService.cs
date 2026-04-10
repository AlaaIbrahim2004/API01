using AutoMapper;
using Domain_Layer.Contracts;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.BasketModule;
using ServicesAbstraction;
using Shared.DTO.BasketModule;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var basketModel = _mapper.Map<BasketDto, Basket>(basket);
            var CreateOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(basketModel);
            if (CreateOrUpdateBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can not Update or Create Basket Now,Try again Later");
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _basketRepository.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var basket = await _basketRepository.GetBasketAsync(Key);
            if (basket == null)
                throw new BasketNotFoundException(Key);
            return _mapper.Map<Basket, BasketDto>(basket);
        }
    }
}
