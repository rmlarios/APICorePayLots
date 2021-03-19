using System;

namespace Dapper.Application.DTOs.RequestModels
{
    public class PagosFechasRequest
    {
        public string Desde { get; set; }
    public string Hasta { get; set; } = "";
    public string IdProyecto { get; set; } = "";
  }
}
