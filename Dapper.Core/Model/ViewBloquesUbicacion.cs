using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewBloquesUbicacion
    {
        public int IdUbicacion { get; set; }
        [Required]
        [StringLength(150)]
        public string Departamento { get; set; }
        [Required]
        [StringLength(150)]
        public string Municipio { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Bloque { get; set; }
        public int IdBloque { get; set; }
        [Column("Id_Municipio")]
        public int? IdMunicipio { get; set; }
        public string NombreProyecto { get; set; }
    }
}
