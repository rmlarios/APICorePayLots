using System;

namespace Dapper.Application.DTOs.RequestModels
{
    public class AnularPagoRequest
    {
        public int IdPago { get; set; }
        public string Observaciones { get; set; }
    }
}
