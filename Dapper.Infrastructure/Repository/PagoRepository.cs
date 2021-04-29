using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Dapper.Core.Model;
using Dapper.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;

namespace Dapper.Infrastructure.Repository
{
  public class PagoRepository : GenericDapperRepository<Pagos>, IPagoRepository
  {
    private readonly IUserAccesor _userAccesor;
    public PagoRepository(IConfiguration configuration, PayLotsDBContext context, IUserAccesor userAccesor) : base(configuration, context)
    {
      _userAccesor = userAccesor;
    }

    public override async Task<int> AddUpdateAsync(int id, Pagos pago)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdPago", id, dbType: DbType.Int32);
      queryParameters.Add("@IdAsignacion", pago.IdAsignacion, dbType: DbType.Int32);
      queryParameters.Add("@NumeroRecibo", pago.NumeroRecibo);
      queryParameters.Add("@FechaRecibo", pago.FechaRecibo);
      queryParameters.Add("@MesPagado", pago.MesPagado);
      queryParameters.Add("@Moneda", pago.Moneda);
      queryParameters.Add("@TasaCambio", pago.TasaCambio);
      queryParameters.Add("@MontoPago", pago.MontoPago);
      queryParameters.Add("@TotalPagado", pago.MontoEfectivo);
      queryParameters.Add("@Interes", pago.Interés);
      queryParameters.Add("@Mora", pago.Mora);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_PagoCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public override async Task<bool> DeleteAsync(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdPago", id);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_PagoEliminar", queryParameters);
      return true;
    }

    public async Task<List<TicketPago>> GenerarTicket(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdPago", id);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteReader<TicketPago>("SP_TicketPagoGenerar", queryParameters);
      return result;
    }

    public async Task<List<Asignacion_PlandePago>> GenerarPlanPago(int id,string opcion)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAsignacion", id);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@opcion", opcion);
      var result = await ExecuteReader<Asignacion_PlandePago>("SP_PlanPagoGenerar", queryParameters);
      //var result = await ExecuteReader<Asignacion_PlandePago>("SP_EstadoCuentaGenerar", queryParameters);
      return result;
    }

    public async Task<List<ViewGraficoPagos>> GetGraficoPagosAsync(string FiltroFechaGrafico)
    {
      string query = "";
      if (FiltroFechaGrafico != null)
      {
        query = "select NombreProyecto + ' ' +Fecha as NombreProyecto,sum(Pagado) as Pagado from View_GraficoPagos where Fecha in('" + FiltroFechaGrafico + "') group by NombreProyecto + ' ' +Fecha";
        //query = "select NombreProyecto,sum(Pagado) as Pagado from View_GraficoPagos group by NombreProyecto";        
      }
      else
      {
        query = "SELECT [NombreProyecto], SUM([Pagado]) Pagado FROM [View_GraficoPagos] GROUP BY NombreProyecto";
      }
      var result = await ExecuteReader<ViewGraficoPagos>(query, new DynamicParameters(), CommandType.Text);
      return result;

    }

    public async Task<List<ViewPagosAsignaciones>> GetPagosFechasAsync(PagosFechasRequest request)
    {
      var result = new List<ViewPagosAsignaciones>();
      if (request.IdProyecto == "")
      {
        if (request.Desde != "" && request.Hasta != "")
          result = await FindAsync<ViewPagosAsignaciones>(m => m.FechaRecibo >= Convert.ToDateTime(request.Desde) && m.FechaRecibo <= Convert.ToDateTime(request.Hasta) && m.FechaRecibo != null && m.EstadoPago=="Vigente");
        else if (request.Desde != "" && request.Hasta == "")
          result = await FindAsync<ViewPagosAsignaciones>(m => m.FechaRecibo.Value.Date == Convert.ToDateTime(request.Desde) && m.FechaRecibo != null && m.EstadoPago=="Vigente");
      }
      else
      {
        if (request.Desde != "" && request.Hasta != "")
          result = await FindAsync<ViewPagosAsignaciones>(m => m.FechaRecibo >= Convert.ToDateTime(request.Desde) && m.FechaRecibo <= Convert.ToDateTime(request.Hasta) && m.FechaRecibo != null && m.IdUbicacion.ToString()==request.IdProyecto && m.EstadoPago=="Vigente");
        else if (request.Desde != "" && request.Hasta == "")
          result = await FindAsync<ViewPagosAsignaciones>(m => m.FechaRecibo.Value.Date == Convert.ToDateTime(request.Desde) && m.FechaRecibo != null && m.IdUbicacion.ToString()==request.IdProyecto && m.EstadoPago=="Vigente");
      }
      

      return result;



    }

    public async Task AnularPago(AnularPagoRequest request)
    {
       string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdPago", request.IdPago);
      queryParameters.Add("@Observaciones", request.Observaciones);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_PagoAnular", queryParameters);
      //return true;
    }
  }
}
