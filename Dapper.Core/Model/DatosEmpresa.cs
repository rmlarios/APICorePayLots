using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class DatosEmpresa
    {
        public int DatosEmpresaId { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        [StringLength(50)]
        public string Telefono { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "image")]
        public byte[] Logo { get; set; }
        [StringLength(100)]
        public string Ruc { get; set; }
        public int? ReciboInicial { get; set; }
        [Column("UAR")]
        [StringLength(50)]
        public string Uar { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime? Far { get; set; }
        [Column("UUA")]
        [StringLength(50)]
        public string Uua { get; set; }
        [Column("FUA", TypeName = "smalldatetime")]
        public DateTime? Fua { get; set; }
    }
}
