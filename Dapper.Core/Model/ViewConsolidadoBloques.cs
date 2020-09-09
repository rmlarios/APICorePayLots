using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewConsolidadoBloques
    {
        public int IdUbicacion { get; set; }
        public string NombreProyecto { get; set; }
        [Column("Id_Municipio")]
        public int? IdMunicipio { get; set; }
        [Required]
        public string Direccion { get; set; }
        public int IdBloque { get; set; }
        [Required]
        [StringLength(50)]
        public string Bloque { get; set; }
        public int? Disponibles { get; set; }
        public int? Asignados { get; set; }
    }
}
