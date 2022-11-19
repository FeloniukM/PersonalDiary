using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PersonalDiary.BLL.Exeptions;
using System.Net;

namespace PersonalDiary.WebAPI.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                switch(exception)
                {
                    case HttpException ex:
                        await HandleHttpException(context, ex);
                        break;
                    case ValidationException ex:
                        await HandleValidationException(context, ex);
                        break;
                    case SecurityTokenException ex:
                        await HandleSecurityTokenException(context, ex);
                         break;
                    case ArgumentNullException ex:
                        await HandleArgumentNullException(context, ex);
                        break;
                }
            }
        }

        private async Task HandleHttpException(HttpContext context, HttpException ex)
        {
            await CreateExceptionAsync(context, ex.StatusCode, new { error = ex.Message });
            return;
        }

        private async Task HandleSecurityTokenException(HttpContext context, SecurityTokenException ex)
        {
            await CreateExceptionAsync(context, HttpStatusCode.BadRequest, new { error = ex.Message });
            return;
        }

        private async Task HandleArgumentNullException(HttpContext context, ArgumentNullException ex)
        {
            await CreateExceptionAsync(context, HttpStatusCode.BadRequest, new { error = ex.Message });
            return;
        }

        private async Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            var message = ex.Errors.Any()
                    ? ex.Errors.Aggregate("", (x, y) => x += $"{y.ErrorMessage}\n")
                    : ex.Message;
            await CreateExceptionAsync(context, HttpStatusCode.BadRequest, message);
        }

        private async Task CreateExceptionAsync(HttpContext context,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            object? errorBody = null)
        {
            errorBody ??= new { error = "Unknown error has occured" };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorBody));
        }
    }
}
