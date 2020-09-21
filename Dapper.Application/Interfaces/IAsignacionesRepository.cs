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
        public Task AnularAsignacion (AnularAsignacionRequest request);
    }
}
