using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Application.DTOs.Account;
using Dapper.Application.Interfaces.Account;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Dapper.Infraestructure.Identity.Models;
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
    [HttpPost("Create")]
    public async Task<Response<ApplicationUserModel>> RegisterAsync(ApplicationUserModel request)
    {
      var origin = Request.Headers["origin"];
      return await _accountService.RegisterAsync(request, origin);
    }
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
    {
      var origin = Request.Headers["origin"];
      return Ok(await _accountService.ConfirmEmailAsync(userId, code));
    }
    [HttpGet("getallusers")]
    public async Task<Response<ApplicationUserModel>> GetAllUsers()
    {
      return await _accountService.GetAllUser();
    }
    [HttpGet("{id}")]
    public async Task<Response<ApplicationUserModel>> GetbyId(string id)
    {
        return new Response<ApplicationUserModel>(await _accountService.GetByIdAsync(id));
    }
    [HttpPut("{id}")]
    public async Task<Response<ApplicationUserModel>> UpdateAsync(string id,ApplicationUserModel request)
    {
      return await _accountService.UpdateUser(id, request);
    }
    [HttpGet("GetRoles")]
    public async Task<Response<string>> GetRoles()
    {
      return new Response<string>(await _accountService.GetRoles());
    }
    [HttpPost("ChangePassword")]
    public async Task<Response<ChangePasswordRequest>> ChangePasswordAsync(ChangePasswordRequest request)
    {
      return await _accountService.ChangePassword(request);
    }
    [HttpPost("ForgotPassword")]
    public async Task<Response<ForgotPasswordRequest>> ForgotPassword(ForgotPasswordRequest request)
    {
      var origin = Request.Headers["origin"];
      await _accountService.ForgotPassword(request.Email, origin);
      return new Response<ForgotPasswordRequest>("Correo Enviado",true);
    }
    [HttpPost("ResetPassword")]
    public async Task<Response<ResetPasswordRequest>> ResetPassword(ResetPasswordRequest request)
    {
      return await _accountService.ResetPassword(request);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteById(string id)
    {
      var obj = await _accountService.GetByIdAsync(id);
      await _accountService.DisableUser(id);
      return new Response<string>("Eliminado Correctamente",true);
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
