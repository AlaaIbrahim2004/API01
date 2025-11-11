namespace ServicesAbstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationService authenticationService { get; }
        public IOrderService orderService { get; }
    }
}
