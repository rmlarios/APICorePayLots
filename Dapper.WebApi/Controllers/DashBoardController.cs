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
        public DashBoardController(IGenericDapperRepository<ViewDashBoard> repo): base(repo)
        {
            
        }

        /// GET api/GetbyUbicaciones
    /// <summary>
    /// Obtiene los Lotes registrados y su estado actual
    /// </summary>    
    /// <returns>Lista de Bloques filtrados</returns>
    [HttpGet("GetSeguimientos")]
    public async Task<Response<Seguimientos>> GetSeguimientos()
    {      
      return new Response<Seguimientos>(await _repo.FindAsync<Seguimientos>());      
    }     
    }
}
