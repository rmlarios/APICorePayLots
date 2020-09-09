using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Entities
{
    public class Lotes
    {
        public Lotes()
        {
            Asignaciones = new Collection<Asignaciones>();
        }
        [Key]
        public int IdLote { get; set; }
        public int? IdBloque { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroLote { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Area { get; set; }
        public string Estado { get; set; }
        

        [ForeignKey("IdBloque")]
        [InverseProperty("Lotes")]
        public Bloques IdBloqueNavigation { get; set; }
        [InverseProperty("IdLoteNavigation")]
        public ICollection<Asignaciones> Asignaciones { get; set; }
    }
}
