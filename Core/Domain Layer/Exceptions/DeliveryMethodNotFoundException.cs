namespace Domain_Layer.Exceptions
{
    public class DeliveryMethodNotFoundException(int id) : NotFoundException($"No delivery Method Found wtth Id ={id}")
    {
    }
}
