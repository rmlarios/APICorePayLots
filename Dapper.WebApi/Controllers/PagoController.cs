using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <returns>Listado de Pagos filtrados</returns>
    [HttpGet("GetListado")]
    public async Task<Response<ViewPagosAsignaciones>> GetListado(bool vigentes)
    {
      if (vigentes)
        return new Response<ViewPagosAsignaciones>(await _repository.FindAsync<ViewPagosAsignaciones>(a => a.Donado == false && a.Estado == "Vigente"));
      else
        return new Response<ViewPagosAsignaciones>(await _repository.FindAsync<ViewPagosAsignaciones>(a => a.MesPagado != null && a.Donado == false));
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
    public async Task<Response<Asignacion_PlandePago>> GetPlanPago(int id)
    {           
      var result = await _repository.Filter("SELECT * FROM FN_Asignacion_PlandePago('" + id + "')");
       if(((JArray)(JsonConvert.DeserializeObject(result))).Count==0)
          throw new KeyNotFoundException("Asignación no encontrada.");

      List<Asignacion_PlandePago> plandePago = new List<Asignacion_PlandePago>();
      JsonConvert.PopulateObject(result, plandePago, new JsonSerializerSettings());
      return new Response<Asignacion_PlandePago>(plandePago);
    }



  }
}