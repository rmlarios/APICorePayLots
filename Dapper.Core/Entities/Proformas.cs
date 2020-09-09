using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Entities
{
    public class Proformas
    {
        [Key]
        public int IdProforma { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Proyecto { get; set; }
        [StringLength(50)]
        public string Lote { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Area { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? PrecioVara { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Total { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Prima { get; set; }
        public int? Interes { get; set; }
        public int? Plazo { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Financiar { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Cuota { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? InteresAcumulado { get; set; }
        [Column("TotalAPagar", TypeName = "numeric(18, 2)")]
        public decimal? TotalApagar { get; set; }
    }
}
