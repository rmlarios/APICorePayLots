using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class Lotes
    {
        public Lotes()
        {
            Asignaciones = new HashSet<Asignaciones>();
        }

        [Key]
        public int IdLote { get; set; }
        public int? IdBloque { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroLote { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Area { get; set; }
        public string Estado { get; set; }
        [Column("UAR")]
        public string Uar { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime? Far { get; set; }
        [Column("UUA")]
        public string Uua { get; set; }
        [Column("FUA", TypeName = "smalldatetime")]
        public DateTime? Fua { get; set; }

        [ForeignKey(nameof(IdBloque))]
        [InverseProperty(nameof(Bloques.Lotes))]
        public virtual Bloques IdBloqueNavigation { get; set; }
        [InverseProperty("IdLoteNavigation")]
        public virtual ICollection<Asignaciones> Asignaciones { get; set; }
    }
}
