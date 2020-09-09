using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    [Table("ErrorSQL")]
    public partial class ErrorSql
    {
        [Key]
        [Column("IdErrorSQL")]
        public int IdErrorSql { get; set; }
        [Required]
        [Column("ErrorSQL")]
        public string ErrorSql1 { get; set; }
        [Required]
        public string IdentityUser { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Fecha { get; set; }
    }
}
