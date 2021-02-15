using System;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.WebApi.Controllers
{
  public class AbonosPrimaController : BaseController<AbonosPrima>
  {
    private readonly IAbonosPrimaRepository _repository;
    public AbonosPrimaController(IAbonosPrimaRepository repo) : base(repo)
    {
      _repository = repo;
    }

[HttpGet("GetListado")]
    public async Task<Response<ViewAbonosPrima>> GetListado()
    {
      
        return new Response<ViewAbonosPrima>(await _repository.FindAsync<ViewAbonosPrima>());
    }


  }
}
