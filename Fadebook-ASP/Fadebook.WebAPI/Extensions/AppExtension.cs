using Fadebook.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Fadebook.WebAPI.Extensions
{
    public static class AppExtension
    {
        public static void ConfigureErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(currentApp =>
            {
                currentApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Console.WriteLine($"Something went wrong: ${contextFeature.Error}");
                        CustomError appError;
                        if (contextFeature.Error is BaseException)
                        {
                            var exception = (BaseException)contextFeature.Error;
                            appError = new CustomError { Message = exception.Message, StatusCode = exception.StatusCode };
                        }
                        else appError = new CustomError { Message = "Internal server error", StatusCode = (int)StatusCodes.Status500InternalServerError };

                        context.Response.StatusCode = appError.StatusCode;
                        await context.Response.WriteAsync(appError.ToString());
                    }

                });
            });
        }
    }
}
