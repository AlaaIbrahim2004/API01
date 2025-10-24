namespace Domain_Layer.Models.ProductModule
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        //public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
