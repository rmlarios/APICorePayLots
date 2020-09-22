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
  public class UbicacionRepository : GenericDapperRepository<Ubicaciones>, IUbicacionRepository
  {

    private readonly IUserAccesor _userAccesor;
    public UbicacionRepository(IConfiguration configuration, IUserAccesor userAccessor, PayLotsDBContext context) : base(configuration, context)
    {
      _userAccesor = userAccessor;
    }

    public override async Task<int> AddUpdateAsync(Ubicaciones ubicacion)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdUbicacion", ubicacion.IdUbicacion, dbType: DbType.Int32);
      queryParameters.Add("@IdMunicipio", ubicacion.IdMunicipio, dbType: DbType.Int32);
      queryParameters.Add("@NombreProyecto", ubicacion.NombreProyecto);
      queryParameters.Add("@Direccion", ubicacion.Direccion);
      queryParameters.Add("@AplicaInteres", ubicacion.AplicaInteres);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_UbicacionCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public override async Task<bool> DeleteAsync(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdUbicacion", id);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_UbicacionEliminar", queryParameters);
      return true;
    }



  }
}
