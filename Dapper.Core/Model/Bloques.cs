using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class Bloques
    {
        public Bloques()
        {
            Lotes = new HashSet<Lotes>();
        }

        [Key]
        public int IdBloque { get; set; }
        public int IdUbicacion { get; set; }
        [Required]
        [StringLength(50)]
        public string Bloque { get; set; }
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

        [ForeignKey(nameof(IdUbicacion))]
        [InverseProperty(nameof(Ubicaciones.Bloques))]
        public virtual Ubicaciones IdUbicacionNavigation { get; set; }
        [InverseProperty("IdBloqueNavigation")]
        public virtual ICollection<Lotes> Lotes { get; set; }
    }
}
