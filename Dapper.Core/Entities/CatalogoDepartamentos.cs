using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Entities
{
    [Table("Catalogo_Departamentos")]
  public class CatalogoDepartamentos
  {
    public CatalogoDepartamentos()
    {
      CatalogoMunicipios = new Collection<CatalogoMunicipios>();
    }

    [Key]
    [Column("Departamento_Id")]
    public int DepartamentoId { get; set; }
    [Required]
    [StringLength(150)]
    public string Descripcion { get; set; }
    [Required]


    [InverseProperty("Departamento")]
    public ICollection<CatalogoMunicipios> CatalogoMunicipios { get; set; }

  }
}
