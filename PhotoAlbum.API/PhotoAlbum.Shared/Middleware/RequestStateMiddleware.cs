using Microsoft.AspNetCore.Http;
using PhotoAlbum.Shared.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbum.Shared.Middleware
{
    public class RequestStateMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestStateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestState = (IRequestState)context.RequestServices.GetService(typeof(IRequestState));

            var userIdClaim = context.User?.FindFirst("userId");
            if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                requestState.UserId = Guid.Parse(userIdClaim.Value);

            await _next(context);
        }
    }
}
