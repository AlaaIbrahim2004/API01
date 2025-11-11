using AutoMapper;
using Domain_Layer.Contracts;
using Domain_Layer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository basketRepository, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration, _mapper));
        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper, basketRepository, _unitOfWork));
        public IProductService ProductService => _LazyproductService.Value;

        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService authenticationService => _LazyAuthenticationService.Value;

        public IOrderService orderService => _LazyOrderService.Value;
    }
}
