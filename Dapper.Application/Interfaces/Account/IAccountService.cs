using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Application.DTOs.Account;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces.Account
{
  public interface IAccountService
  {
    Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    Task<Response<ApplicationUserModel>> RegisterAsync(ApplicationUserModel request, string origin);
    Task<Response<string>> ConfirmEmailAsync(string userId, string code);
    Task ForgotPassword(string email, string origin);
    Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    Task<Response<ApplicationUserModel>> GetAllUser();
    Task<Response<ApplicationUserModel>> UpdateUser(string userId, ApplicationUserModel request);
    Task<ApplicationUserModel> GetByIdAsync(string id);
    Task<List<string>> GetRoles();
    Task<Response<ChangePasswordRequest>> ChangePassword(ChangePasswordRequest request);
  }
}
