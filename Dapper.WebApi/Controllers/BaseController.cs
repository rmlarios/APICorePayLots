using System.Data;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dapper.Application.Wrappers;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class BaseController<T> : ControllerBase where T : class
  {
    protected IGenericDapperRepository<T> _repo;
    public BaseController(IGenericDapperRepository<T> repo)
    {
      _repo = repo;
    }

    
    // GET api/{T}/Listar
    /// <summary>
    /// Lista todos los registros de la entidad
    /// </summary>
    /// <returns>Lista de registros</returns>
    [HttpGet("Listar")]
    public async Task<Response<T>> GetAll()
    {
      return new Response<T>(await _repo.GetAllAsync());
    }

    // GET api/{T}/5
    /// <summary>
    /// Busca un registro basado en su Key
    /// </summary>
    /// <param name="id">Id a buscar</param>
    /// <returns>El registro buscado</returns>
    [HttpGet("{id}")]
    public async Task<Response<T>> GetById(int id)
    {
      return new Response<T>(await _repo.GetByIdAsync(id));
    }

    // POST api/{T}
    /// <summary>
    /// Agrega un registro a la Base de Datos
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity">Clase con los valor a agregar</param>
    /// <returns>El registro creado</returns>
    [HttpPost("Create")]
    public async Task<Response<T>> PostCreate(int id,T entity)
    {
      var result = await _repo.AddUpdateAsync(id,entity);
      var inserted = await _repo.GetByIdAsync((int)result);
      return new Response<T>(inserted, "Creado Correctamente");
    }

    //PUT api/{T}/5
    /// <summary>
    /// Actualiza un registro de la Base de Datos
    /// </summary>
    /// <param name="id">Key del registro a actulizar</param>
    /// <param name="entity">Clase con los valores a actualizar</param>
    /// <returns>El registro actualizado</returns>
    [HttpPut("{id}")]
    public async Task<Response<T>> PutUpdate(int id,[FromBody] T entity)
    {
      var obj = await _repo.GetByIdAsync(id);
      if (obj != null)
      {
        var result = await _repo.AddUpdateAsync(id,entity);
        return new Response<T>(await _repo.GetByIdAsync(id), "Actualizado Correctamente");
      }
      return new Response<T>("Se ha producido algún error");

    }


    // DELETE api/ubicacion/5
    /// <summary>
    /// Elimina un registro de la Base de Datos
    /// </summary>
    /// <param name="id">Key del registro a eliminar</param>
    /// <returns>Mensaje de confirmación de la acción</returns>
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteById(int id)
    {
      var obj = await _repo.GetByIdAsync(id);
      var result = await _repo.DeleteAsync(id);
      return new Response<string>("Eliminado Correctamente",true);
    }


    /*  // POST api/base
      [HttpPost("")]
      public void Poststring(string value)
      {
      }

      // PUT api/base/5
      [HttpPut("{id}")]
      public void Putstring(int id, string value)
      {
      }

      // DELETE api/base/5
      [HttpDelete("{id}")]
      public void DeletestringById(int id)
      {
      } */
  }
}