using Domain_Layer.Contracts;
using Domain_Layer.Models.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace Presistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _database.KeyDeleteAsync(Key);
        }

        public async Task<Basket?> GetBasketAsync(string Key)
        {
            var basket = await _database.StringGetAsync(Key);
            if (basket.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<Basket>(basket!);
        }
    }
}
