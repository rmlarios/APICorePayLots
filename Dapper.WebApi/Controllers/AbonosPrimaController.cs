using System;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
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

     [HttpGet("GetTicketPrima/{id}")]
    public async Task<Response<TicketPrima>> GetTicket(int id)
    {
      var result = await _repository.GenerarTicketPrima(id);
      return new Response<TicketPrima>(result);
    }


  }
}
