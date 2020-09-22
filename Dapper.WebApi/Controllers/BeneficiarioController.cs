using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Exceptions;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
//using Dapper.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BeneficiarioController : BaseController<Beneficiarios>
  {
    private readonly IBeneficiarioRepository _repository;
    public BeneficiarioController(IBeneficiarioRepository beneficiarioRepository) : base(beneficiarioRepository)
    {
      _repository = beneficiarioRepository;
    }

    // POST api/beneficiario
    [HttpPost("Create")]
    public async Task<Response<Beneficiarios>> PostCreate(Beneficiarios beneficiario)
    {
      var id = await _repository.AddUpdateAsync(beneficiario);
      beneficiario.IdBeneficiario = (int)id;
      return new Response<Beneficiarios>(beneficiario, "Creado Correctamente");
    }

    // PUT api/beneficiario/5
    [HttpPut("{id}")]
    public async Task<Response<Beneficiarios>> PutUpdate(int id, Beneficiarios beneficiario)
    {
      if (id != beneficiario.IdBeneficiario)
        throw new ApiException("Error al tratar de actualizar");
      var obj = await _repository.GetByIdAsync(beneficiario.IdBeneficiario);
      var result = await _repository.AddUpdateAsync(beneficiario);
      return new Response<Beneficiarios>(await _repository.GetByIdAsync(beneficiario.IdBeneficiario), "Actualizado Correctamente");
    }

    // DELETE api/beneficiario/5
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteById(int id)
    {
      var obj = await _repository.GetByIdAsync(id);
      var result = await _repository.DeleteAsync(id);

      return new Response<string>("Eliminado Correctamente");

    }
  }
}