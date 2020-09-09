using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewLotes
    {
        [Required]
        [StringLength(150)]
        public string Municipio { get; set; }
        [Required]
        [StringLength(50)]
        public string Bloque { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroLote { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Area { get; set; }
        public string Estado { get; set; }
        public int IdLote { get; set; }
        [StringLength(81)]
        public string NombreLote { get; set; }
        public int IdBloque { get; set; }
        [Column("Id_Municipio")]
        public int? IdMunicipio { get; set; }
        [Required]
        [StringLength(150)]
        public string Departamento { get; set; }
        public string NombreProyecto { get; set; }
        public string EstadoActual { get; set; }
        public int IdUbicacion { get; set; }
        public string Observaciones { get; set; }
    }
}
