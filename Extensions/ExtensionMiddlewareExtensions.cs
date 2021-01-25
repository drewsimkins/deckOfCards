using deckOfCards.Exceptions;
using deckOfCards.Models;
using deckOfCards.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;


namespace deckOfCards.Extensions
{
    public static class ExtensionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var exception = error.Error;

                        if (exception.GetType() == typeof(InvalidHandException))
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        logger.LogError($"Something went wrong: {error.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = exception.Message
                        }.ToString()) ;
                    }
                });
            });
        }
    }
}
