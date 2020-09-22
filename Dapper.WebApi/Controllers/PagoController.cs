using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;
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
        return new Response<ViewPagosAsignaciones>(await _repository.FindAsync<ViewPagosAsignaciones>(a => a.Donado == false && a.Estado=="Vigente"));
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
      return new Response<Pagos>(await _repository.FindAsync<Pagos>(a => a.IdAsignacion==id));
    }



  }
}