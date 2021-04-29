using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoteController : BaseController<Lotes>
  {
    private readonly ILoteRepository _repository;
    public LoteController(ILoteRepository repository) : base(repository)
    {
      _repository = repository;
    }

    /// GET api/GetbyUbicaciones
    /// <summary>
    /// Obtiene los Lotes registrados y su estado actual
    /// </summary>    
    /// <returns>Lista de Bloques filtrados</returns>
    [HttpGet("GetLotes")]
    public async Task<Response<ViewLotes>> GetLotes(int take, int skip)
    {      
      if(take!=0 || skip!=0)
        return new Response<ViewLotes>(await _repository.FindAsync<ViewLotes>(take, skip), await _repo.GetCount<ViewLotes>());
      else
        return new Response<ViewLotes>(await _repository.FindAsync<ViewLotes>(), await _repo.GetCount<ViewLotes>());      
    }     

  }
}