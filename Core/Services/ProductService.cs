using AutoMapper;
using Domain_Layer.Contracts;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.ProductModule;
using Services.Spcefications;
using ServicesAbstraction;
using Shared;
using Shared.DTO.ProductModule;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }

        public async Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(queryParams);
            var products = await repo.GetAllAsync(specification);
            var productDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var productCount = productDto.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var totalCount = await repo.CountAsync(CountSpec);
            return new PaginationResult<ProductDto>(productCount, queryParams.PageIndex, totalCount, productDto);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var types = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);

        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(id);
            var product = await repo.GetByIdAsync(specification);
            if (product == null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<Product, ProductDto>(product);

        }
    }
}
