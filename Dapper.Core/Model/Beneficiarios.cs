using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class Beneficiarios
    {
        public Beneficiarios()
        {
            Asignaciones = new HashSet<Asignaciones>();
        }

        [Key]
        public int IdBeneficiario { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        public string CedulaIdentidad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
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

        [InverseProperty("IdBeneficiarioNavigation")]
        public virtual ICollection<Asignaciones> Asignaciones { get; set; }
    }
}
