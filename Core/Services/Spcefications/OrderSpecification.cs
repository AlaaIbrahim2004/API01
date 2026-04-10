using Domain_Layer.Models.OrderModule;

namespace Services.Spcefications
{
    internal class OrderSpecification : BaseSpecification<Order, Guid>
    {
        public OrderSpecification(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }
        public OrderSpecification(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
