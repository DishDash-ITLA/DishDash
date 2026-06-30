using DishDash.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace DishDash.API.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error no manejado: {Mensaje}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, titulo) = ex switch
            {
                NotFoundException => (HttpStatusCode.NotFound, "Recurso no encontrado"),
                BusinessException => (HttpStatusCode.BadRequest, "Error de negocio"),
                ConflictException => (HttpStatusCode.Conflict, "Conflicto"),
                UnauthorizedException => (HttpStatusCode.Unauthorized, "No autorizado"),
                _ => (HttpStatusCode.InternalServerError, "Error interno del servidor")
            };

            var response = new
            {
                status = (int)statusCode,
                titulo,
                detalle = ex.Message,
                timestamp = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
            );
        }
    }
}
