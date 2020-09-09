using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Entities
{
  public class Bloques
  {
    public Bloques()
    {
       Lotes = new Collection<Lotes>();
    }

    [Key]
    public int IdBloque { get; set; }
    public int IdUbicacion { get; set; }
    [Required]
    [StringLength(50)]
    public string Bloque { get; set; }
    public string Observaciones { get; set; }


    [ForeignKey("IdUbicacion")]
    [InverseProperty("Bloques")]
    public Ubicaciones IdUbicacionNavigation { get; set; }
    [InverseProperty("IdBloqueNavigation")]
    public ICollection<Lotes> Lotes { get; set; }
  }
}
