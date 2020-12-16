using System.Linq;
using System;
using Dapper.Core.Model;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
using System.Collections.Generic;

namespace Dapper.Application.Interfaces
{
  public interface IPagoRepository : IGenericDapperRepository<Pagos>
  {
    Task<List<Asignacion_PlandePago>> GenerarPlanPago(int id);
    Task<List<TicketPago>> GenerarTicket(int id);
    Task<List<ViewGraficoPagos>> GetGraficoPagosAsync(string FiltroFechaGrafico);
  }
}
