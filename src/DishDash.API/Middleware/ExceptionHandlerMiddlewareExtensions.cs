namespace DishDash.API.Middleware
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
