using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PagoController : BaseController<Pagos>
  {
    private readonly IPagoRepository _repository;
    public PagoController(IPagoRepository repository) : base(repository)
    {
      _repository = repository;
    }

    /// GET api/pago/GetListado
    /// <summary>
    /// Obtiene el listado de pagos segun algunas condiciones
    /// </summary>
    /// <param name="vigentes">Condicional para filtrar asignaciones vigentes</param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <returns>Listado de Pagos filtrados</returns>
    [HttpGet("GetListado")]
    public async Task<Response<ViewPagosAsignaciones>> GetListado(bool vigentes, int take, int skip)
    {
      Expression<Func<ViewPagosAsignaciones, bool>> exp = a => a.Donado == false;
      if (vigentes)
        exp = a => a.Donado == false && a.Estado == "Vigente";
      else
        exp = a => a.MesPagado != null && a.Donado == false;
      //Expression<Func<ViewPagosAsignaciones, object>> sort = a => a.IdAsignacion;
      if (take != 0 || skip != 0)
      {
        return new Response<ViewPagosAsignaciones>(await _repository.FindAsync<ViewPagosAsignaciones>(take, skip, "IdPago",exp), await _repository.GetCount<ViewPagosAsignaciones>(exp));
      }
      else
      {
        return new Response<ViewPagosAsignaciones>(await _repository.FindAsync<ViewPagosAsignaciones>(exp));
      }
    }

    /// GET api/pago/GetByAsignacion
    /// <summary>
    /// Lista los pagos asociados a una Asignacion
    /// </summary>
    /// <param name="id">Key de la Asignacion a filtrar</param>
    /// <returns>Listado de Pagos filtrados por Asignacion</returns>
    [HttpGet("GetByAsignacion/{id}")]
    public async Task<Response<Pagos>> GetByAsignacion(int id)
    {
      return new Response<Pagos>(await _repository.FindAsync<Pagos>(a => a.IdAsignacion == id));
    }

    /// <summary>
    /// Genera el Plan de Pago de una Asignacion
    /// </summary>
    /// <param name="id">Key de la Asignacion</param>
    /// <returns>Plan de Pago de la Asignacion solicitada</returns>
    [HttpGet("GetPlanPago/{id}")]
    public async Task<Response<Asignacion_PlandePago>> GetPlanPago(int id,string opcion)
    {
      /*var result = await _repository.Filter("SELECT * FROM FN_Asignacion_PlandePago('" + id + "')");
       if(((JArray)(JsonConvert.DeserializeObject(result))).Count==0)
          throw new KeyNotFoundException("Asignación no encontrada.");

      List<Asignacion_PlandePago> plandePago = new List<Asignacion_PlandePago>();
      JsonConvert.PopulateObject(result, plandePago, new JsonSerializerSettings());
      return new Response<Asignacion_PlandePago>(plandePago);*/
      var result = await _repository.GenerarPlanPago(id,opcion);
      return new Response<Asignacion_PlandePago>(result);
    }

    /// <summary>
    /// Lista los meses pendientes de pago e incluye el mes del pago cargado
    /// </summary>
    /// <param name="id">key de la Asignacion</param>
    /// <param name="idpago">key del pago</param>
    /// <returns>Datos de los meses a pagar</returns>
    [HttpGet("GetMesesPagar/{id}")]
    public async Task<Response<Asignacion_PlandePago>> GetMesesPagar(int id, int idpago)
    {
      var result = await _repository.Filter("SELECT MesPagado,Estado,MontoMinimo,ISNULL(Interes,0) Interes,ISNULL(Mora,0) Mora,TotalPagar FROM FN_Asignacion_PlandePago('" + id + "') WHERE (MesPagado!='Prima' and Estado !='Cancelado') OR MesPagado= (SELECT MesPagado FROM Pagos WHERE IdPago=" + idpago + ")");
      if (((JArray)(JsonConvert.DeserializeObject(result))).Count == 0)
        throw new KeyNotFoundException("Asignación no encontrada.");

      List<Asignacion_PlandePago> plandePago = new List<Asignacion_PlandePago>();
      JsonConvert.PopulateObject(result, plandePago, new JsonSerializerSettings());
      return new Response<Asignacion_PlandePago>(plandePago);
    }

    /// <summary>
    /// Obtiene los datos del ticket de pago del parametro
    /// </summary>
    /// <param name="id">Id del Pago a Generar Ticket</param>
    /// <returns></returns>
    [HttpGet("GetTicket/{id}")]
    public async Task<Response<TicketPago>> GetTicket(int id)
    {
      var result = await _repository.GenerarTicket(id);
      return new Response<TicketPago>(result);
    }

    /// <summary>
    /// Obtiene los datos de los graficos de pagos por fecha
    /// </summary>
    /// <param name="fechapago">Fecha a obtener datos</param>
    /// <returns></returns>
    [HttpGet("GetGrafico")]
    public async Task<Response<ViewGraficoPagos>> GetGrafico(string fechapago)
    {
      var result = await _repository.GetGraficoPagosAsync(fechapago);
      return new Response<ViewGraficoPagos>(result);
    }

    /// <summary>
    /// Obtiene listado de Asignaciones en Mora
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetMorosos")]
    public async Task<Response<ViewReporteMorosos>> GetMorosos()
    {
      //var result = ;
      return new Response<ViewReporteMorosos>(await _repository.FindAsync<ViewReporteMorosos>(m => m.CuotasRequeridas > m.Cuotas));
    }

    /// <summary>
    /// Obtiene los pagos entre determinadas fechas y proyecto seleccionado1
    /// </summary>
    /// <param name="request">Parametros para generar resultados</param>
    /// <returns></returns>
    [HttpGet("GetPagosFechas")]
    public async Task<Response<ViewPagosAsignaciones>> GetPagosFechas([FromQuery] PagosFechasRequest request)
    {
      var result = await _repository.GetPagosFechasAsync(request);
      return new Response<ViewPagosAsignaciones>(result);
    }


    [HttpPost("Anular")]
    public async Task<Response<string>> PostAnular (AnularPagoRequest request)
    {
      await _repository.AnularPago(request);
      return new Response<string>("Anulado Correctamente",true);
    }



  }
}