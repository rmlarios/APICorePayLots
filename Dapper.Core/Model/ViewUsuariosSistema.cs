using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Infrastructure.Model
{
    public partial class ViewUsuariosSistema
    {
        public Guid UserId { get; set; }
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }
        [StringLength(256)]
        public string PasswordQuestion { get; set; }
        [StringLength(128)]
        public string PasswordAnswer { get; set; }
        public bool IsLockedOut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastLoginDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
