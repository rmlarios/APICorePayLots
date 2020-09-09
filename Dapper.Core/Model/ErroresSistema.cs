using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ErroresSistema
    {
        [Key]
        public int IdErrorSistema { get; set; }
        [Required]
        public string Error { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Fecha { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Pantalla { get; set; }
        [Required]
        public string CodError { get; set; }
    }
}
