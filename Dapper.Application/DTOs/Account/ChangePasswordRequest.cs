using System;
using System.ComponentModel.DataAnnotations;

namespace Dapper.Application.DTOs.Account
{
    public class ChangePasswordRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string currentpsw { get; set; }
        [Required]    
        public string newpsw { get; set; }
    }
}
