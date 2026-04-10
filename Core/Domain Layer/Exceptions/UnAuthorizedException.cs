namespace Domain_Layer.Exceptions
{
    public class UnAuthorizedException(string message = "Invalid Email or Password") : Exception(message)
    {
    }
}
