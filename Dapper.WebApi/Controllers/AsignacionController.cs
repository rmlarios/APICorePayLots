using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionController : ControllerBase
    {
        private readonly IAsignacionesRepository _repository;
        public AsignacionController(IAsignacionesRepository repository)
        {
            _repository = repository;
        }

        // GET api/asignacion/listar
        [HttpGet("Listar")]
        public async Task<IReadOnlyList<Asignaciones>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/asignacion/5
        [HttpGet("{id}")]
        public async Task<Asignaciones> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpGet("ByBenef/{id}")]
        public async Task<List<ViewAsignacionesLotes>> GetbyBenef(int id)
        {
            return await _repository.GetAsignacionesBeneficiario(id);
        }

        // POST api/asignacion
        [HttpPost("Create")]
        public async Task<Response<Asignaciones>> PostCreate(Asignaciones asignacion)
        {
            var result = await _repository.AddUpdateAsync(asignacion);
            return new Response<Asignaciones>(asignacion,"Creado Correctamente");
        }

        // PUT api/asignacion/5
        [HttpPut("{id}")]
        public async Task<Response<Asignaciones>> PutUpdate(Asignaciones asignacion)
        {
            var obj = await _repository.GetByIdAsync(asignacion.IdAsignacion);
            if (obj!=null)
            {
                var result = await _repository.AddUpdateAsync(asignacion);
                return new Response<Asignaciones>(await _repository.GetByIdAsync(asignacion.IdAsignacion),"Actualizado Correctamente");
            }
            return new Response<Asignaciones>("Se ha producido algún error");

        }

        // DELETE api/asignacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var obj = await _repository.GetByIdAsync(id);
            if(obj!=null)
            {
                var result = await _repository.DeleteAsync(id);
                if (result) return Ok("Registro Eliminado."); else return NotFound();
            }

            return BadRequest("Se ha producido algún erro.")

        }
    }
}