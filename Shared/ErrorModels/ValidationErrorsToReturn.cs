using System.Net;

namespace Shared.ErrorModels
{
    public class ValidationErrorsToReturn
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string ErrorMessage { get; set; } = "Validation Failed";
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];

    }
}
