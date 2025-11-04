using AutoMapper;
using Domain_Layer.Contracts;
using ServicesAbstraction;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _LazyproductService.Value;
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));

        public IBasketService BasketService => _LazyBasketService.Value;
    }
}
