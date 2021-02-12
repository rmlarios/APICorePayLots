using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DatosEmpresaController : BaseController<DatosEmpresa>
  {
    private readonly IDatosEmpresaRepository _repo;
    public DatosEmpresaController(IDatosEmpresaRepository repo) : base(repo)
    {
      _repo = repo;
    }

   [HttpPost("Backup")]
    public async Task<Response<string>> PostBackup (RespaldoRequest request)
    {
      var result = await _repo.BackupAsync(request);
      return new Response<string>(result,true);
    }

  }
}