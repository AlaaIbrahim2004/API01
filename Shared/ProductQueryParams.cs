namespace Shared
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions? SortingOptions { get; set; }
        public string? SearchValue { get; set; }

    }
}
