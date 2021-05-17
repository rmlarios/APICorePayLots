using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces
{
    public interface IAbonosPrimaRepository : IGenericDapperRepository<AbonosPrima>
    {
        Task<List<TicketPrima>> GenerarTicketPrima(int id);
    }
}
