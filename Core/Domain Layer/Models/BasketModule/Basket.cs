namespace Domain_Layer.Models.BasketModule
{
    public class Basket
    {
        public string Id { get; set; }//GUID ,Created from frontEnd
        public ICollection<BasketItem> Items { get; set; }
    }
}
