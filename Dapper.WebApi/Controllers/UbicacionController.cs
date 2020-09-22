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
    [HttpGet("GetUbicaciones")]
    public async Task<Response<ViewConsolidadoUbicaciones>> GetUbicaciones()
    {      
      return new Response<ViewConsolidadoUbicaciones>(await _repository.FindAsync<ViewConsolidadoUbicaciones>());      
    }

    // POST api/ubicacion
    [HttpPost("Create")]
    public async Task<Response<Ubicaciones>> PostCreate(Ubicaciones ubicacion)
    {
      var result = await _repository.AddUpdateAsync(ubicacion);
      ubicacion.IdUbicacion = (int)result;
      return new Response<Ubicaciones>(ubicacion, "Creado Correctamente");
    }

    // PUT api/ubicacion/5
    [HttpPut("{id}")]
    public async Task<Response<Ubicaciones>> PutUpdate(Ubicaciones ubicacion)
    {
      var obj = await _repository.GetByIdAsync(ubicacion.IdUbicacion);
      if (obj != null)
      {
        var result = await _repository.AddUpdateAsync(ubicacion);
        return new Response<Ubicaciones>(await _repository.GetByIdAsync(ubicacion.IdUbicacion), "Actualizado Correctamente");
      }
      return new Response<Ubicaciones>("Se ha producido algún error");

    }        

    // DELETE api/ubicacion/5
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteById(int id)
    {
      var obj = await _repository.GetByIdAsync(id);
      var result = await _repository.DeleteAsync(id);
      return new Response<string>("Eliminado Correctamente");
    }
  }
}