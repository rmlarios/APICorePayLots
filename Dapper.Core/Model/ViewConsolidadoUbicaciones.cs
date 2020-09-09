using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewConsolidadoUbicaciones
    {
        public int IdUbicacion { get; set; }
        public string NombreProyecto { get; set; }
        [Column("Id_Municipio")]
        public int? IdMunicipio { get; set; }
        [Required]
        public string Direccion { get; set; }
        public int? Bloques { get; set; }
        public int? Asignados { get; set; }
        public int? Diponibles { get; set; }
        public bool? AplicaInteres { get; set; }
        public string Proyecto { get; set; }
    }
}
