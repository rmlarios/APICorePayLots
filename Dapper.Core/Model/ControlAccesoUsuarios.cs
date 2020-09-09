using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    [Table("Control_Acceso_Usuarios")]
    public partial class ControlAccesoUsuarios
    {
        [Key]
        public int IdLogueo { get; set; }
        [Column("Sesion_Id")]
        public string SesionId { get; set; }
        public string Usuario { get; set; }
        public string Perfil { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime? Far { get; set; }
        [Column("PC")]
        public string Pc { get; set; }
    }
}
