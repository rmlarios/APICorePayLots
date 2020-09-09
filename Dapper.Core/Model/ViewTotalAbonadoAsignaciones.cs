using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewTotalAbonadoAsignaciones
    {
        [Column(TypeName = "numeric(38, 2)")]
        public decimal? Abonado { get; set; }
        public int? Cuotas { get; set; }
        public string Estado { get; set; }
        public int IdAsignacion { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroLote { get; set; }
    }
}
