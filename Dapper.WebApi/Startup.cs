using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Dapper.Infraestructure.Identity;
using Dapper.Infrastructure;
using Dapper.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Dapper.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddInfrastructure(Configuration);
      services.AddIdentityInfrastructure(Configuration);
      services.AddSwaggerExtension();
      services.AddControllers()
          .AddNewtonsoftJson(o =>
          {
            o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
          })
          .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
          .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

      services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
             {
               builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
             }));



    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //app.UseHttpsRedirection();
      app.UseCors("MyPolicy");

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseSwaggerExtension();
      app.UseErrorHandlingMiddleware();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

     


    }
  }
}
