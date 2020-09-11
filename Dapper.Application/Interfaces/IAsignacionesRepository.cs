using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Dapper.Core.Entities;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces
{
    public interface IAsignacionesRepository : IGenericDapperRepository<Asignaciones>
    {
        public Task<List<ViewAsignacionesLotes>>  GetAsignacionesBeneficiario (int id) ;

        public Task<ViewAsignacionesSaldo> GetDatosAsignacion (int id);
    }
}
