using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces
{
  public interface IProformaRepository : IGenericDapperRepository<Proformas>
  {
    Task<List<Proformas>> GenerarProforma(int id);
  }
}
