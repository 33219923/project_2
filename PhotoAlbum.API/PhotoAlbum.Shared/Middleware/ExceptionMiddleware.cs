using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhotoAlbum.Shared.Exceptions;
using PhotoAlbum.Shared.Models;
using System;
using System.Net;
using System.Threading.Tasks;


namespace PhotoAlbum.Shared.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var error = new ErrorDetails()
            {
                Message = ex switch
                {
                    EntityNotFoundException => ex.Message,
                    InvalidRequestException => ex.Message,
                    _ => "An error occurred. Please refer to the logs."
                },
                StatusCode = ex switch
                {
                    EntityNotFoundException => (int)HttpStatusCode.NotFound,
                    InvalidRequestException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                }
#if DEBUG
                ,
                Debug = ex
#endif
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;
            await context.Response.WriteAsync(error.ToString());
        }
    }
}
