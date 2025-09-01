using Forum.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Forum.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionToStatusMap = new()
        {
            { typeof(UserNotFoundException), HttpStatusCode.NotFound },
            { typeof(PostNotFoundException), HttpStatusCode.NotFound },
            { typeof(EmailAlreadyExistsException), HttpStatusCode.BadRequest },
            { typeof(UsernameAlreadyExistsException), HttpStatusCode.BadRequest },
            { typeof(InvalidCredentialsException), HttpStatusCode.Unauthorized },
            { typeof(CannotLikeOwnPostException), HttpStatusCode.BadRequest },
            { typeof(TagNotFoundException), HttpStatusCode.BadRequest },
            { typeof(DuplicateTagException), HttpStatusCode.BadRequest }
        };

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            if (ExceptionToStatusMap.TryGetValue(exception.GetType(), out HttpStatusCode code))
            {
                statusCode = code;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            string message;
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                message = "An unexpected error occurred.";
            }
            else
            {
                message = exception.Message;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            string jsonResponse = JsonSerializer.Serialize(new
            {
                Success = false,
                Error = message
            });

            return context.Response.WriteAsync(jsonResponse);
        }

    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
