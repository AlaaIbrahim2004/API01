using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DTO.ProductModule;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }


    }
}
