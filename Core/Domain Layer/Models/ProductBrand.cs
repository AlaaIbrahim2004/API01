namespace Domain_Layer.Models
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        //public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
