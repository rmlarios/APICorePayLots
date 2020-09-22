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

    // GET api/GetbyUbicaciones
    [HttpGet("GetbyUbicacion/{id}")]
    public async Task<Response<ViewConsolidadoBloques>> GetbyUbicaciones(int id)
    {      
      return new Response<ViewConsolidadoBloques>(await _repository.FindAsync<ViewConsolidadoBloques>(a => a.IdUbicacion==id));      
    }

   /*  // POST api/bloque
    [HttpPost("Create")]
    public async Task<Response<Ubicaciones>> PostCreate(Bloques bloque)
    {
      var result = await _repository.AddUpdateAsync(bloque);
      ubicacion.IdUbicacion = (int)result;
      return new Response<Ubicaciones>(ubicacion, "Creado Correctamente");
    } */

    // PUT api/ubicacion/5
    [HttpPut("{id}")]
    public async Task<Response<Bloques>> PutUpdate(Bloques bloque)
    {
      var obj = await _repository.GetByIdAsync(bloque.IdBloque);
      if (obj != null)
      {
        var result = await _repository.AddUpdateAsync(bloque);
        return new Response<Bloques>(await _repository.GetByIdAsync(bloque.IdBloque), "Actualizado Correctamente");
      }
      return new Response<Bloques>("Se ha producido algún error");

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