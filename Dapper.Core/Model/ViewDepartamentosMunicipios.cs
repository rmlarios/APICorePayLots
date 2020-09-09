using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class ViewDepartamentosMunicipios
    {
        [Column("Departamento_Id")]
        public int DepartamentoId { get; set; }
        [Required]
        [StringLength(150)]
        public string Departamento { get; set; }
        [Column("Municipio_Id")]
        public int MunicipioId { get; set; }
        [Required]
        [StringLength(150)]
        public string Municipio { get; set; }
    }
}
