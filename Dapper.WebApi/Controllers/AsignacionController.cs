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
using Dapper.Application.Exceptions;
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

    
    /// <summary>
    /// Listar Asignaciones
    /// </summary>    
    /// <returns>Lista de asignaciones con datos de saldo y abonos</returns>
    [HttpGet("GetAsignaciones")]
    public async Task<Response<ViewAsignacionesSaldo>> GetAsignaciones(int take, int skip)
    {
      //return new Response<ViewAsignacionesSaldo>(await _repository.GetAllData<ViewAsignacionesSaldo>());
      return new Response<ViewAsignacionesSaldo>(await _repository.FindAsync<ViewAsignacionesSaldo>(take, skip));
    }

    // GET api/GetbyBenef/5
    /// <summary>
    /// Listar Asignaciones por Beneficiario
    /// </summary>
    /// <param name="id">Key del Beneficiario</param>
    /// <returns>Lista de asignaciones obtenidas</returns>
    [HttpGet("GetbyBenef/{id}")]
    public async Task<Response<ViewAsignacionesLotes>> GetbyBenef(int id)
    {
     
      return new Response<ViewAsignacionesLotes>(await _repository.FindAsync<ViewAsignacionesLotes>(a=>a.IdBeneficiario==id));
      //return new Response<ViewAsignacionesLotes>(await _repository.GetAsignacionesBeneficiario(id));
    }

    //GET api/GetbyId
    /// <summary>
    /// Obtiene los datos de una Asignacion
    /// </summary>
    /// <param name="id">Key de la Asignacion a buscar</param>
    /// <returns>Datos de la asignacion</returns>
    [HttpGet("GetDatosbyId/{id}")]
    public async Task<Response<ViewAsignacionesSaldo>> GetDatosbyId(int id)
    { 
      Expression<Func<ViewAsignacionesSaldo,bool>> exp = a =>a.IdAsignacion == id;
      var result = await _repository.FindAsync<ViewAsignacionesSaldo>(exp);
      if(result.Count==0)
       throw new ApiException("Registro no encontrado");

      return new Response<ViewAsignacionesSaldo>(result);
    } 

    //POST api/asignacion/Anular/5
    /// <summary>
    /// Anular una Asignacion
    /// </summary>
    /// <param name="request">Key de la Asignacion a anular y Motivo de Anulación</param>
    /// <returns>Mensaje de confirmacion</returns>
    [HttpPost("Anular")]
    public async Task<Response<string>> PostAnular (AnularAsignacionRequest request)
    {
      await _repository.AnularAsignacion(request);
      return new Response<string>("Anulada Correctamente",true);
    }

    //POST api/asignacion/Activar/5
    /// <summary>
    /// Activa una Asignacion que ha sido Anulada
    /// </summary>
    /// <param name="request">Key de la Asignacion a activar</param>
    /// <returns>Mensaje de confirmacion</returns>
    [HttpPost("Activar")]
    public async Task<Response<string>> PostActivar (ActivarRequest request)
    {
      await _repository.ActivarAsignacion(request);
      return new Response<string>("Activada Correctamente",true);
    }

    
    [HttpGet("GetEstadoCuenta/{id}")]
    public async Task<Response<EstadoCuenta>> GetEstadoCuenta(int id)
    {
      var result = await _repository.GenerarEstadoCuenta(id);
      return new Response<EstadoCuenta>(result);
    }

  }
}