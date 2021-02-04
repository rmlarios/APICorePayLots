using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Model
{
  public class ViewDashBoard
  {
    public int proyectos { get; set; }
    public int lotes { get; set; }
    public int clientes { get; set; }
    [Column(TypeName = "numeric(38, 2)")]
    public decimal montodolares { get; set; }
    [Column(TypeName = "numeric(38, 2)")]
    public decimal montocordobas { get; set; }
  }
}
