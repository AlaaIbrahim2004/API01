namespace Domain_Layer.Exceptions
{
    public class BasketNotFoundException(string id) : NotFoundException($"Basket with Id = {id} is not found")
    {

    }
}
