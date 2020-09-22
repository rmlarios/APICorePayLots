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
  public class UbicacionController : BaseController<Ubicaciones>
  {
    private readonly IUbicacionRepository _repository;
    public UbicacionController(IUbicacionRepository repository) : base(repository)
    {
      _repository = repository;
    }

    // GET api/GetUbicaciones
    /// <summary>
    /// Lista los proyectos registrados con el detalle de Lotes
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetUbicaciones")]
    public async Task<Response<ViewConsolidadoUbicaciones>> GetUbicaciones()
    {      
      return new Response<ViewConsolidadoUbicaciones>(await _repository.FindAsync<ViewConsolidadoUbicaciones>());      
    }
   
  }
}