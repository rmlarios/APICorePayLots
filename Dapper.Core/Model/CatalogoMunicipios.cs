using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    [Table("Catalogo_Municipios")]
    public partial class CatalogoMunicipios
    {
        [Key]
        [Column("Municipio_Id")]
        public int MunicipioId { get; set; }
        [Column("Departamento_Id")]
        public int DepartamentoId { get; set; }
        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Required]
        [Column("UUA")]
        [StringLength(50)]
        public string Uua { get; set; }
        [Column("FUA", TypeName = "smalldatetime")]
        public DateTime Fua { get; set; }
        [Required]
        [Column("UAR")]
        [StringLength(50)]
        public string Uar { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime Far { get; set; }

        [ForeignKey(nameof(DepartamentoId))]
        [InverseProperty(nameof(CatalogoDepartamentos.CatalogoMunicipios))]
        public virtual CatalogoDepartamentos Departamento { get; set; }
    }
}
