using System;
using System.ComponentModel.DataAnnotations;

namespace Dapper.Core.Model
{
    public class AbonosPrima
    {
        [Key]
        public int IdAbonoPrima { get; set; }

        public int IdAsignacion { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Monto { get; set; }
  }
}
