using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DatosEmpresaController : BaseController<DatosEmpresa>
  {
    public DatosEmpresaController(IDatosEmpresaRepository repo) : base(repo)
    {
    }
  }
}