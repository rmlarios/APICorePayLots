using System;
using Microsoft.AspNetCore.Builder;

namespace Dapper.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.WebApi");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.WebApi");
            });
        }
    }
}
