using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Dapper.WebApi.Middlewares
{
  public class ErrorHandlerMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next,ILogger<Log> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception error)
      {
        var response = context.Response;
        response.ContentType = "application/json";
        var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
        

        switch (error)
        {
          case Application.Exceptions.ApiException e:
            // custom application error
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            break;
          case ValidationException e:
            // custom application error
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            //responseModel.Errors = e;
            break;
          case KeyNotFoundException e:
            // not found error
            response.StatusCode = (int)HttpStatusCode.NotFound;
            break;
          default:
            // unhandled error
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError(error, error?.Message);
            break;
        }
        var result = JsonSerializer.Serialize(responseModel);

        await response.WriteAsync(result);
      }
    }

  }
}
