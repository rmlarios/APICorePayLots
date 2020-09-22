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
  public class LoteRepository : GenericDapperRepository<Lotes>, ILoteRepository
  {
    private readonly IUserAccesor _userAccesor;
    public LoteRepository(IConfiguration configuration, PayLotsDBContext context,IUserAccesor userAccesor) : base(configuration, context)
    {
        _userAccesor=userAccesor;
    }

    public override async Task<int> AddUpdateAsync(Lotes lote)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdLote", lote.IdLote, dbType: DbType.Int32);
      queryParameters.Add("@IdBloque", lote.IdBloque, dbType: DbType.Int32);
      queryParameters.Add("@NumeroLote", lote.NumeroLote);
      queryParameters.Add("@Area", lote.Area);      
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_LoteCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public override async Task<bool> DeleteAsync(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdLote", id);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_LoteEliminar", queryParameters);
      return true;
    }


  }
}
