namespace Domain_Layer.Exceptions
{
    public sealed class ProductNotFoundException(int id) : NotFoundException($"Product with id = {id} is Not Found")
    {
    }
}
