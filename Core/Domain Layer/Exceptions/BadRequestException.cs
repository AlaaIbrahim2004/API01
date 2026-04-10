namespace Domain_Layer.Exceptions
{
    public class BadRequestException(List<string> errors) : Exception("Validation Faild")
    {
        public List<string> Errors { get; } = errors;
    }
}
