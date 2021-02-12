using System;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces
{
  public interface IDatosEmpresaRepository : IGenericDapperRepository<DatosEmpresa>
  {
    Task<string> BackupAsync(RespaldoRequest request);
  }
}