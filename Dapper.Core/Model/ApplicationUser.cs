using System;

namespace Dapper.Core.Model
{
    public class ApplicationUser : IdentityUser
    {
    
         public bool? IsActive { get; set; }        
    
    }
}
