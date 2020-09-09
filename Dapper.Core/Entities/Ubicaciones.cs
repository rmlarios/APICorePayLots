using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Dapper.Core.Entities
{
  public class Ubicaciones
  {
    public Ubicaciones()
    {
      Bloques = new Collection<Bloques>();
    }
    [Key]
    public int IdUbicacion { get; set; }
    [Column("Id_Municipio")]
    public int? IdMunicipio { get; set; }
    public string NombreProyecto { get; set; }
    [Required]
    public string Direccion { get; set; }
    public bool? AplicaInteres { get; set; }

    [InverseProperty("IdUbicacionNavigation")]
    public ICollection<Bloques> Bloques { get; set; }
  }
}
