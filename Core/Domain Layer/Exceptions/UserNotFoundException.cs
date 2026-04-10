namespace Domain_Layer.Exceptions
{
    public sealed class UserNotFoundException(string email) : NotFoundException($"email {email} is not found, try create account")
    {
    }
}
