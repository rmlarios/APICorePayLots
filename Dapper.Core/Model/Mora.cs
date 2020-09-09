using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
    public partial class Mora
    {
        [Key]
        public int IdMora { get; set; }
        public int? Minimo { get; set; }
        public int? Maximo { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Porcentaje { get; set; }
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
