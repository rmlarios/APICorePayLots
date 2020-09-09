using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Entities
{
      public partial class Seguimientos
    {
        [Key]
        public int IdSeguimiento { get; set; }
        [Required]
        public string Observaciones { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Column("IndexSQL")]
        public int IndexSql { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime Far { get; set; }
        [Column("UAR")]
        [StringLength(50)]
        public string Uar { get; set; }
    }
}
