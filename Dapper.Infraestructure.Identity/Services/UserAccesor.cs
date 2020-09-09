using System;
using System.Linq;
using System.Security.Claims;
using Dapper.Application.Interfaces.Account;
using Microsoft.AspNetCore.Http;

namespace Dapper.Infraestructure.Identity.Services
{
  public class UserAccesor : IUserAccesor
  {
      private readonly IHttpContextAccessor _httpContextAccessor;
      public UserAccesor(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor; 
      }
    public string GetCurrentUser()
    {
      var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return username;
    }
  }
}
