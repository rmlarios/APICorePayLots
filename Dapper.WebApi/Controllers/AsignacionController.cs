using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class AsignacionController : BaseController<Asignaciones>
  {
    private readonly IAsignacionesRepository _repository;

    public enum Estados
    {
      Vigente,
      Anulada,
      Otro
    }



    public AsignacionController(IAsignacionesRepository repository) : base(repository)
    {
      _repository = repository;
    }

    // GET api/asignacion/listar
    /* [HttpGet("Listar")]
    public async Task<IReadOnlyList<Asignaciones>> GetAll()
    {
      return await _repository.GetAllAsync();
    } */

    // GET api/asignacion/5
    /* [HttpGet("{id}")]
    public async Task<Asignaciones> GetById(int id)
    {
      return await _repository.GetByIdAsync(id);
    } */

    // GET api/ByBenef/5
    [HttpGet("GetporBenef/{id}")]
    public async Task<Response<ViewAsignacionesLotes>> GetbyBenef(int id)
    {
      return new Response<ViewAsignacionesLotes>(await _repository.GetAsignacionesBeneficiario(id));
    }

    //GET api/GetByState
    [HttpGet("GetDatosAsignacion")]
    public async Task<Response<ViewAsignacionesSaldo>> GetDatosAsignaciones(int id)
    {      
      return new Response<ViewAsignacionesSaldo>(await _repository.GetDatosAsignacion(id));      
    }

    // POST api/asignacion
    [HttpPost("Create")]
    public async Task<Response<Asignaciones>> PostCreate(Asignaciones asignacion)
    {
      var result = await _repository.AddUpdateAsync(asignacion);
      asignacion.IdAsignacion = (int) result;
      return new Response<Asignaciones>(asignacion, "Creado Correctamente");
    }

    // PUT api/asignacion/5
    [HttpPut("{id}")]
    public async Task<Response<Asignaciones>> PutUpdate(Asignaciones asignacion)
    {
      var obj = await _repository.GetByIdAsync(asignacion.IdAsignacion);
      if (obj != null)
      {
        var result = await _repository.AddUpdateAsync(asignacion);
        return new Response<Asignaciones>(await _repository.GetByIdAsync(asignacion.IdAsignacion), "Actualizado Correctamente");
      }
      return new Response<Asignaciones>("Se ha producido algún error");

    }

    // DELETE api/asignacion/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
      var obj = await _repository.GetByIdAsync(id);
      if (obj != null)
      {
        var result = await _repository.DeleteAsync(id);
        if (result) return Ok("Registro Eliminado."); else return NotFound();
      }

      return BadRequest("Se ha producido algún error.");

    }
  }
}