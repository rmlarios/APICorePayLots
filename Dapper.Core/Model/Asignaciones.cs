using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class Asignaciones
    {
        public Asignaciones()
        {
            Pagos = new HashSet<Pagos>();
        }

        [Key]
        public int IdAsignacion { get; set; }
        public int IdLote { get; set; }
        public int IdBeneficiario { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicioPago { get; set; }
        public int? DiaCuota { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? MontoTotal { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? CuotaMinima { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Prima { get; set; }
        public string Estado { get; set; }
        public bool? Donado { get; set; }
        public bool? AplicaInteres { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? TasaInteres { get; set; }
        public bool? AplicaMora { get; set; }
        public int? Plazo { get; set; }
        public string Observaciones { get; set; }
        [Required]
        [Column("UAR")]
        public string Uar { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime Far { get; set; }
        [Required]
        [Column("UUA")]
        public string Uua { get; set; }
        [Column("FUA", TypeName = "smalldatetime")]
        public DateTime Fua { get; set; }

        [ForeignKey(nameof(IdBeneficiario))]
        [InverseProperty(nameof(Beneficiarios.Asignaciones))]
        public virtual Beneficiarios IdBeneficiarioNavigation { get; set; }
        [ForeignKey(nameof(IdLote))]
        [InverseProperty(nameof(Lotes.Asignaciones))]
        public virtual Lotes IdLoteNavigation { get; set; }
        [InverseProperty("IdAsignacionNavigation")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
