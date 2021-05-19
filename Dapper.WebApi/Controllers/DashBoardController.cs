using System;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.WebApi.Controllers
{
    public class DashBoardController : BaseController<ViewDashBoard>
    {
      private readonly IDashBoardRepository _repository;
        public DashBoardController(IDashBoardRepository repo): base(repo)
        {
      _repository = repo;
    }

        /// GET api/GetbyUbicaciones
    /// <summary>
    /// Obtiene los Lotes registrados y su estado actual
    /// </summary>    
    /// <returns>Lista de Bloques filtrados</returns>
    [HttpGet("GetSeguimientos")]
    public async Task<Response<Seguimientos>> GetSeguimientos()
    {      
      return new Response<Seguimientos>(await _repository.FindAsync<Seguimientos>());      
    }     
    }
}
