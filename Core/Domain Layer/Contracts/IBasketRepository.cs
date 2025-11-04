using Domain_Layer.Models.BasketModule;

namespace Domain_Layer.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string Key);
        Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
