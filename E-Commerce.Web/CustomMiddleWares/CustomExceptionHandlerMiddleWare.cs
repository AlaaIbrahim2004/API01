using Domain_Layer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //Request
                await _next.Invoke(httpContext);
                //Response
                await HelperEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                await HandelExceptionAsync(httpContext, ex);
            }

        }

        private static async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Set Status Code for Response 
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            //Set Content Type Response 
            //httpContext.Response.ContentType = "application/json";
            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message,
                Errors = ex switch
                {
                    BadRequestException badRequestException => badRequestException.Errors,
                    _ => []
                }
            };
            //Transfer object to JSON
            //var responseJson = JsonSerializer.Serialize(response);

            //await httpContext.Response.WriteAsync(responseJson);
            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static async Task HelperEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
