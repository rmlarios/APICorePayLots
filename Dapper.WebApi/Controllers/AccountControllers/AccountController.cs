using System;
using System.Threading.Tasks;
using Dapper.Application.DTOs.Account;
using Dapper.Application.Interfaces.Account;
using Dapper.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.WebApi.Controllers.AccountControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return await _accountService.AuthenticateAsync(request, GenerateIPAddress());
        }
        [HttpPost("register")]
        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return await _accountService.RegisterAsync(request,origin);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery]string userId, [FromQuery]string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }

        

    private string GenerateIPAddress()
    {
      if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
  }
}
