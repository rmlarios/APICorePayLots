using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Dapper.Core.Model;
using Dapper.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;

namespace Dapper.Infrastructure.Repository
{
  public class MoraRepository : GenericDapperRepository<Mora>, IMoraRepository
  {
    private readonly IUserAccesor _userAccesor;
    public MoraRepository(IConfiguration configuration, PayLotsDBContext context,IUserAccesor userAccesor) : base(configuration, context)
    {
        _userAccesor = userAccesor;
    }

    public override async Task<int> AddUpdateAsync(int id,Mora mora)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdMora", id, dbType: DbType.Int32);
      queryParameters.Add("@Minimo", mora.Minimo, dbType: DbType.Int32);
      queryParameters.Add("@Maximo", mora.Maximo, dbType: DbType.Int32);
      queryParameters.Add("@Porcentaje", mora.Porcentaje, dbType: DbType.Decimal);      
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_MorasCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public override async Task<bool> DeleteAsync(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdMora", id);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_MorasEliminar", queryParameters);
      return true;
    }
  }
}
