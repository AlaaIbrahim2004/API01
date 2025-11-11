using Shared.DTO.OrderModule;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
