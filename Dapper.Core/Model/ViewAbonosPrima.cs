using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public class ViewAbonosPrima
    {
         [Key]
        public int IdAbonoPrima { get; set; }
        [Required]
        public int IdAsignacion{ get; set; }

        public string NombreCompleto { get; set; }
        public string NombreLote { get; set; }
        [Column(TypeName = "smalldatetime")]
        [Required]
        public DateTime? Fecha { get; set; }
        [Required]
         [Column(TypeName = "numeric(18, 2)")]
        public decimal? Monto { get; set; }

         [Column(TypeName = "numeric(18, 2)")]
        public decimal? Prima { get; set; }

  }
}
