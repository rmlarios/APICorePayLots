using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Core.Model;
//using Dapper.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiarioController : ControllerBase
    {
        private readonly IBeneficiarioRepository _repository;
        public BeneficiarioController(IBeneficiarioRepository beneficiarioRepository) 
        {
            _repository = beneficiarioRepository;
        }

        // GET api/beneficiario
        [HttpGet("Listar")]
        public async Task<IReadOnlyList<Beneficiarios>> GetAll()
        {
            return  await _repository.GetAllAsync();
        }

        // GET api/beneficiario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _repository.GetByIdAsync(id);
            /*if (r==null)
                return NotFound();*/
            return Ok(r);

        }

        // POST api/beneficiario
        [HttpPost("Create")]
        public async Task<IActionResult> PostCreate(Beneficiarios beneficiario)
        {
            var id =await _repository.AddUpdateAsync(beneficiario);
            return CreatedAtAction( nameof(GetById),new {id = id},beneficiario);
        }

        // PUT api/beneficiario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstring(int id, Beneficiarios beneficiario)
        {
            if(id!=beneficiario.IdBeneficiario)
                return BadRequest();
            var result = await _repository.AddUpdateAsync(beneficiario);
            return Ok("Cambios guadados");
        }

        // DELETE api/beneficiario/5
        [HttpDelete("{id}")]
        public void DeletestringById(int id)
        {
        }
    }
}