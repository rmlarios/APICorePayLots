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
  public class BloqueRepository : GenericDapperRepository<Bloques>, IBloqueRepository
  {

    private readonly IUserAccesor _userAccesor;
    public BloqueRepository(IConfiguration configuration, IUserAccesor userAccessor, PayLotsDBContext context) : base(configuration, context)
    {
      _userAccesor = userAccessor;
    }

    public override async Task<int> AddUpdateAsync(Bloques bloque)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdBloque", bloque.IdBloque, dbType: DbType.Int32);
      queryParameters.Add("@IdUbicacion", bloque.IdUbicacion, dbType: DbType.Int32);
      queryParameters.Add("@Bloque", bloque.Bloque);
      queryParameters.Add("@Observaciones", bloque.Observaciones);      
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_BloqueCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public override async Task<bool> DeleteAsync(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdBloque", id);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_BloqueEliminar", queryParameters);
      return true;
    }
  }
}
