using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class Ubicaciones
    {
        public Ubicaciones()
        {
            Bloques = new HashSet<Bloques>();
        }

        [Key]
        public int IdUbicacion { get; set; }
        [Column("Id_Municipio")]
        public int? IdMunicipio { get; set; }
        public string NombreProyecto { get; set; }
        [Required]
        public string Direccion { get; set; }
        public bool? AplicaInteres { get; set; }
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

        [InverseProperty("IdUbicacionNavigation")]
        public virtual ICollection<Bloques> Bloques { get; set; }
    }
}
