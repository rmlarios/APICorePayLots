using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewGraficoPagos
    {
        public string NombreProyecto { get; set; }
        [Column(TypeName = "numeric(38, 2)")]
        public decimal? Pagado { get; set; }
        [StringLength(20)]
        public string Fecha { get; set; }
    }
}
