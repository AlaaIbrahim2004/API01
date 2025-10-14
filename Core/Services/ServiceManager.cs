using AutoMapper;
using Domain_Layer.Contracts;
using ServicesAbstraction;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _LazyproductService.Value;
    }
}
