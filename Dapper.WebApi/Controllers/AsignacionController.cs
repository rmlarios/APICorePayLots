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
using Dapper.Application.DTOs.RequestModels;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AsignacionController : BaseController<Asignaciones>
  {
    private readonly IAsignacionesRepository _repository;

    public AsignacionController(IAsignacionesRepository repository) : base(repository)
    {
      _repository = repository;
    }

    // GET api/GetbyBenef/5
    [HttpGet("GetbyBenef/{id}")]
    public async Task<Response<ViewAsignacionesLotes>> GetbyBenef(int id)
    {      
      return new Response<ViewAsignacionesLotes>(await _repository.FindAsync<ViewAsignacionesLotes>(a=>a.IdBeneficiario==id));
      //return new Response<ViewAsignacionesLotes>(await _repository.GetAsignacionesBeneficiario(id));
    }

    //GET api/GetbyId
    [HttpGet("GetbyId/{id}")]
    public async Task<Response<ViewAsignacionesSaldo>> GetbyId(int id)
    {     
      Expression<Func<ViewAsignacionesSaldo,bool>> exp = a =>a.IdAsignacion == id;
      return new Response<ViewAsignacionesSaldo>(await _repository.FindAsync<ViewAsignacionesSaldo>(exp));
    }

    // POST api/asignacion
    [HttpPost("Create")]
    new public async Task<Response<Asignaciones>> PostCreate(Asignaciones asignacion)
    {
      var result = await _repository.AddUpdateAsync(asignacion);
      asignacion.IdAsignacion = (int) result;
      return new Response<Asignaciones>(asignacion, "Creado Correctamente");
    }

    //POST api/asignacion/Anular/5
    [HttpPost("Anular/{id}")]
    public async Task<Response<string>> PostAnular (AnularAsignacionRequest request)
    {
      await _repository.AnularAsignacion(request);
      return new Response<string>("Anulada Correctamente");
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
  }
}