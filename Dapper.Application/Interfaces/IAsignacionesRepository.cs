using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
//using Dapper.Core.Entities;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces
{
  
  public interface IAsignacionesRepository : IGenericDapperRepository<Asignaciones>
  {
    Task AnularAsignacion(AnularAsignacionRequest request);
    Task<List<EstadoCuenta>> GenerarEstadoCuenta(int id,string opcion);    
    Task ActivarAsignacion(ActivarRequest request);

    Task<List<Asignacion_PlandePago>> GenerarPlanInicial(int id);
  }
}
