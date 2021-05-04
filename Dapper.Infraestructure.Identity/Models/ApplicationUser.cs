using Microsoft.AspNetCore.Identity;

namespace Dapper.Infraestructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
         public bool IsActive { get; set; }
                 
    }
}