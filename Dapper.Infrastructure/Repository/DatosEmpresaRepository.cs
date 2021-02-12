using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Application.DTOs.RequestModels;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Dapper.Core.Model;
using Dapper.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;

namespace Dapper.Infrastructure.Repository
{
  public class DatosEmpresaRepository : GenericDapperRepository<DatosEmpresa>, IDatosEmpresaRepository
  {
    private readonly IUserAccesor _userAccesor;
    public DatosEmpresaRepository(IConfiguration configuration, PayLotsDBContext context,IUserAccesor userAccesor) : base(configuration, context)
    {
        _userAccesor = userAccesor;
    }

    public override async Task<int> AddUpdateAsync(int id,DatosEmpresa datos)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@NombreEmpresa", datos.NombreEmpresa);
      queryParameters.Add("@Direccion", datos.Direccion);
      queryParameters.Add("@Telefono", datos.Telefono);
      queryParameters.Add("@Email", datos.Email);
      queryParameters.Add("@Ruc", datos.Ruc);
      if(datos.Logo!=null)
        queryParameters.Add("@Logo", datos.Logo);
      
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_DatosEmpresaCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public async Task<string> BackupAsync(RespaldoRequest request)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@Path", request.Path);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("BK", queryParameters);
      if(result!=null) return result.ToString(); else return "Error al crear respaldo";
    }  
  }
}
