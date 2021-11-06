using Microsoft.AspNetCore.Mvc;
using PhotoAlbum.Shared.Models;
using System.Linq;
using System.Net;

namespace PhotoAlbum.Shared.Middleware
{
    public static class ValidationExceptionHandler
    {
        public static BadRequestObjectResult Handle(ActionContext actionContext)
        {
            var error = new ValidationError()
            {
                Message = "Parameters invalid.",
                StatusCode = (int)HttpStatusCode.BadRequest,
            };

            foreach (var validationError in actionContext.ModelState.Where(modelError => modelError.Value?.Errors?.Count > 0))
            {
                var msg = string.Join(" ", validationError.Value.Errors.Select(x => x.ErrorMessage));

                if (validationError.Key.Length == 0)
                    error.Message = msg;
                else
                    error.FieldErrors.Add(char.ToLowerInvariant(validationError.Key[0]) + validationError.Key[1..], msg);
            }

            return new BadRequestObjectResult(error);
        }
    }
}
