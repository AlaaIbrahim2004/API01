namespace Domain_Layer.Exceptions
{
    public sealed class AddressNotFoundException(string userName) : NotFoundException($"User {userName} has no address")
    {
    }
}
