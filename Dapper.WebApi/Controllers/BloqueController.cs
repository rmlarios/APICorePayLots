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
  public class BloqueController : BaseController<Bloques>
  {
    private readonly IBloqueRepository _repository;
    public BloqueController(IBloqueRepository repository) : base(repository)
    {
      _repository = repository;
    }

    /// GET api/GetbyUbicaciones
    /// <summary>
    /// Obtiene los Bloques por Proyetco
    /// </summary>
    /// <param name="id">Id del proyecto a filtrar</param>
    /// <returns>Lista de Bloques filtrados</returns>
    [HttpGet("GetbyUbicacion/{id}")]
    public async Task<Response<ViewConsolidadoBloques>> GetbyUbicaciones(int id)
    {      
      return new Response<ViewConsolidadoBloques>(await _repository.FindAsync<ViewConsolidadoBloques>(a => a.IdUbicacion==id));      
    }     

  }
}