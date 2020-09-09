using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    [Table("Catalogo_Preguntas")]
    public partial class CatalogoPreguntas
    {
        [Key]
        public int IdCatalogoPregunta { get; set; }
        [Required]
        public string Pregunta { get; set; }
        [Column("FAR", TypeName = "smalldatetime")]
        public DateTime Far { get; set; }
        [Column("UAR")]
        [StringLength(50)]
        public string Uar { get; set; }
    }
}
