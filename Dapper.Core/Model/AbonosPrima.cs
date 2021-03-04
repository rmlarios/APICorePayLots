using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public class AbonosPrima
    {
        [Key]
        public int IdAbonoPrima { get; set; }

        public int IdAsignacion { get; set; }
        public DateTime Fecha { get; set; }
        [Column(TypeName = "numeric(18, 4)")]
        public Decimal Monto { get; set; }
  }
}
