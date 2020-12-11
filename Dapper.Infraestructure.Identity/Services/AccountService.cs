using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper.Application.DTOs.Account;
using Dapper.Application.DTOs.Email;
using Dapper.Application.Exceptions;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Dapper.Core.Settings;
using Dapper.Infraestructure.Identity.Helpers;
using Dapper.Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;

namespace Dapper.Infraestructure.Identity.Services
{
  public class AccountService : IAccountService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly JWTSettings _jwtSettings;
    //private readonly IDateTimeService _dateTimeService;
    public AccountService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JWTSettings> jwtSettings,
        //IDateTimeService dateTimeService, 
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _jwtSettings = jwtSettings.Value;
      //dateTimeService = dateTimeService;
      _signInManager = signInManager;
      this._emailService = emailService;
    }

    public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user == null)
      {
        user = await _userManager.FindByNameAsync(request.Email);
        if (user == null)
          //throw new ApiException($"No Accounts Registered with {request.Email}.");
          throw new ApiException($"Credenciales Incorrectas.");
      }

      if (user.IsActive == false)
      {
        throw new ApiException($"Usuario Inválido.");
      }
      var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
      if (!result.Succeeded)
      {
        //throw new ApiException($"Invalid Credentials for '{request.Email}'.");
        throw new ApiException($"Credenciales Incorrectas.");
      }
      if (!user.EmailConfirmed)
      {
        //throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
        throw new ApiException($"Credenciales Incorrectas.");
      }
      JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
      AuthenticationResponse response = new AuthenticationResponse();
      response.Id = user.Id;
      response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
      response.Email = user.Email;
      response.UserName = user.UserName;
      var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
      response.Roles = rolesList.ToList();
      response.IsVerified = user.EmailConfirmed;
      var refreshToken = GenerateRefreshToken(ipAddress);
      response.RefreshToken = refreshToken.Token;
      return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
    }

    public async Task<Response<ApplicationUserModel>> RegisterAsync(ApplicationUserModel request, string origin)
    {
      var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
      if (userWithSameUserName != null)
      {
        throw new ApiException($"Nombre de Usuario '{request.UserName}' ya está en uso.");
      }
      var user = new ApplicationUser
      {
        Email = request.Email,
        IsActive = request.IsActive,
        //FirstName = request.FirstName,
        //LastName = request.LastName,
        UserName = request.UserName
      };
      request.Password = "Tempo@123";
      var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
      if (userWithSameEmail == null)
      {
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
          //await _userManager.AddToRoleAsync(user, "Administrador");
          await AddUserToRoleAsync(user, "Operador");
          var verificationUri = await SendVerificationEmail(user, origin);
          Uri url = new Uri(verificationUri);
          //TODO: Attach Email Service here and configure it via appsettings
          string Msg = $"Active su cuenta <a href='http://" + HtmlEncoder.Default.Encode(url.AbsoluteUri) + "'>haciendo click aqui</a>.";
          await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = Msg, Subject = "Confirmar Registro." });
          return new Response<ApplicationUserModel>(request, message: $"Usuario Registrado. Debe confirmar su cuenta accediendo antes de poder loguearse");
        }
        else
        {
          throw new ApiException($"{result.Errors}");
        }
      }
      else
      {
        throw new ApiException($"Email {request.Email } ya esta registrado.");
      }
    }

    private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
    {
      //var userClaims = await _userManager.GetClaimsAsync(user);

      var roles = await _userManager.GetRolesAsync(user);

      var roleClaims = new List<Claim>();

      for (int i = 0; i < roles.Count; i++)
      {
        roleClaims.Add(new Claim("roles", roles[i]));
      }

      string ipAddress = IpHelper.GetIpAddress();

      var claims = new[]
      {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
      //.Union(userClaims)
      .Union(roleClaims);

      var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
      var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

      var jwtSecurityToken = new JwtSecurityToken(
          issuer: _jwtSettings.Issuer,
          audience: _jwtSettings.Audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
          signingCredentials: signingCredentials);
      return jwtSecurityToken;
    }

    private string RandomTokenString()
    {
      using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
      var randomBytes = new byte[40];
      rngCryptoServiceProvider.GetBytes(randomBytes);
      // convert random bytes to hex string
      return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
    {
      var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
      var route = "identity/account/confirmemail/";
      var _enpointUri = new Uri(string.Concat($"{origin}/", route));
      var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
      verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
      //Email Service Call Here
      return verificationUri;
    }

    public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
    {
      var user = await _userManager.FindByIdAsync(userId);
      code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
      var result = await _userManager.ConfirmEmailAsync(user, code);
      if (result.Succeeded)
      {
        return new Response<string>(user.Id, message: $"Cuenta confirmada para {user.Email}. Ahora puede usar el /api/Account/authenticate endpoint.");
      }
      else
      {
        throw new ApiException($"Ha ocurrido un error al confirmar la cuenta de  {user.Email}.");
      }
    }

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
      return new RefreshToken
      {
        Token = RandomTokenString(),
        Expires = DateTime.UtcNow.AddDays(7),
        Created = DateTime.UtcNow,
        CreatedByIp = ipAddress
      };
    }

    public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
    {
      var account = await _userManager.FindByEmailAsync(model.Email);

      // always return ok response to prevent email enumeration
      if (account == null) return;

      var code = await _userManager.GeneratePasswordResetTokenAsync(account);
      var route = "api/account/reset-password/";
      var _enpointUri = new Uri(string.Concat($"{origin}/", route));
      var emailRequest = new EmailRequest()
      {
        Body = $"You reset token is - {code}",
        To = model.Email,
        Subject = "Reset Password",
      };
      await _emailService.SendAsync(emailRequest);
    }

    public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
    {
      var account = await _userManager.FindByEmailAsync(model.Email);
      if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
      var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
      if (result.Succeeded)
      {
        return new Response<string>(model.Email, message: $"Password Resetted.");
      }
      else
      {
        throw new ApiException($"Error occured while reseting the password.");
      }
    }

    public async Task<Response<ApplicationUserModel>> GetAllUser()
    {
      var users = await _userManager.Users.ToListAsync();
      List<ApplicationUserModel> models = new List<ApplicationUserModel>();
      ApplicationUserModel model = new ApplicationUserModel();
      foreach (var u in users)
      {
        model = new ApplicationUserModel();
        model.UserName = u.UserName;
        model.Email = u.Email;
        model.IsActive = u.IsActive;
        model.EmailConfirmed = u.EmailConfirmed;
        var roles = await _userManager.GetRolesAsync(u);
        if (roles.Count != 0)
          model.Rol = roles.ToList()[0];

        models.Add(model);
      }
      /*.Select(u => new ApplicationUserModel
      {
        Id = u.Id,
        UserName = u.UserName,
        Email = u.Email,
        IsActive = u.IsActive,
        EmailConfirmed = u.EmailConfirmed,
        Rol = _roleManager.roles
      }
    ).ToListAsync();*/

      // List<ApplicationUserModel> users_ = users;
      return new Response<ApplicationUserModel>(models);
      //throw new NotImplementedException();
    }
    public async Task<Response<ApplicationUserModel>> UpdateUser(string userId, ApplicationUserModel request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user == null)
      {
        user = await _userManager.FindByNameAsync(request.Email);
        if (user == null)
          //throw new ApiException($"No Accounts Registered with {request.Email}.");
          throw new ApiException($"Usuario no Existe.");
      }
      user.UserName = request.UserName;
      user.Email = request.Email;
      user.IsActive = request.IsActive;
      await _userManager.UpdateAsync(user);

      return new Response<ApplicationUserModel>(request);
    }
    public async Task<ApplicationUserModel> GetByIdAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user == null)
        throw new ApiException("Registro no encontrado");
      return new ApplicationUserModel()
      {
        UserName = user.UserName,
        Email = user.Email,
        IsActive = user.IsActive
      };

    }

    public async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
    {
      await CheckRoleAsync(roleName);
      await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task CheckRoleAsync(string roleName)
    {
      var roleExists = await _roleManager.RoleExistsAsync(roleName);
      if (!roleExists)
      {
        await _roleManager.CreateAsync(new IdentityRole
        {
          Name = roleName
        });
      }
    }
    public async Task<IList<string>> GetUserRol(ApplicationUser user)
    {
      return await _userManager.GetRolesAsync(user);
    }

    public async Task<List<string>> GetRoles()
    {
      return await _roleManager.Roles.Select(m=>m.Name).ToListAsync();
    }
  }
}