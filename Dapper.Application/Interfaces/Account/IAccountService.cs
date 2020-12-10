using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Application.DTOs.Account;
using Dapper.Application.Wrappers;

namespace Dapper.Application.Interfaces.Account
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<Response<List<M>>> GetAllUser<M>() where M : class;
  }
}
