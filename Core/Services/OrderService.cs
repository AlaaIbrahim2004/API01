using AutoMapper;
using Domain_Layer.Contracts;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.OrderModule;
using Domain_Layer.Models.ProductModule;
using Services.Spcefications;
using ServicesAbstraction;
using Shared.DTO.IdentityModule;
using Shared.DTO.OrderModule;

namespace Services
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            var address = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);
            if (basket is null)
                throw new BasketNotFoundException(orderDto.BasketId);
            List<OrderItem> orderItems = new List<OrderItem>();
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(item.id);
                if (product is null)
                    throw new ProductNotFoundException(item.id);
                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = product.Id,
                        PictureUrl = product.PictureUrl,
                        ProductName = product.Name
                    },
                    Price = product.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
            }
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);
            var order = new Order(email, address, deliveryMethod, orderItems, subTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }
        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var spec = new OrderSpecification(email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }
        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
            return _mapper.Map<Order, OrderToReturnDto>(order);

        }
    }
}
