using System.Linq;
using System;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces
{
    public interface IPagoRepository : IGenericDapperRepository<Pagos>
    {
    }
}
