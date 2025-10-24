using Domain_Layer.Models.ProductModule;
using Shared;

namespace Services.Spcefications
{
    internal class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
          : base(p => (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId)
          && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
          && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
        }
    }
}
