using System;

namespace Dapper.Core.Model
{
  public class ApplicationUserModel
  {
    public string Id { get; set; }
    public bool IsActive { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string Rol { get; set; } = "";

  }
}
