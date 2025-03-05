using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext,e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;

            if (e.GetType() == typeof(ValidationException))
            {
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                return httpContext.Response.WriteAsync(new ErrorDataDetails<IEnumerable<ValidationFailure>>
                {
                    Success = false,
                    Message = "Validation Error",
                    Data = errors
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                Success = false,
                Message = message
            }.ToString());
        }
    }
}
