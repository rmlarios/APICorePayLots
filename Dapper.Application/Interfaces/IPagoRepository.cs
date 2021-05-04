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
    Task<List<EstadoCuenta>> GenerarPlanPago(int id,string opcion);
    Task<List<TicketPago>> GenerarTicket(int id);
    Task<List<ViewGraficoPagos>> GetGraficoPagosAsync(string FiltroFechaGrafico);
    Task<List<ViewPagosAsignaciones>> GetPagosFechasAsync(PagosFechasRequest request);
    Task AnularPago(AnularPagoRequest request);
  }
}
