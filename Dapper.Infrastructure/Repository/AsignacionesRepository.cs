using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
//using Dapper.Core.Entities;
using Dapper.Core.Model;
using Dapper.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Dapper.Application.DTOs.RequestModels;

namespace Dapper.Infrastructure.Repository
{
  public class AsignacionesRepository : GenericDapperRepository<Asignaciones>, IAsignacionesRepository
  {
    private readonly IUserAccesor _userAccesor;

   
    //private readonly PayLotsDBContext _context;
    public AsignacionesRepository(IConfiguration configuration, IUserAccesor userAccesor, PayLotsDBContext context) : base(configuration, context)
    {
      _userAccesor = userAccesor;
      //_context = context;
    }

    public override async Task<int> AddUpdateAsync(int id, Asignaciones asignacion)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAsignacion", id, dbType: DbType.Int32);
      queryParameters.Add("@IdBeneficiario", asignacion.IdBeneficiario);
      queryParameters.Add("@IdLote", asignacion.IdLote);
      queryParameters.Add("@FechaInicioPago", asignacion.FechaInicioPago);
      queryParameters.Add("@MontoTotal", asignacion.MontoTotal);
      queryParameters.Add("@CuotaMinima", asignacion.CuotaMinima);
      queryParameters.Add("@Prima", asignacion.Prima);
      queryParameters.Add("@Donado", asignacion.Donado);
      queryParameters.Add("@AplicaInteres", asignacion.AplicaInteres);
      queryParameters.Add("@AplicaMora", asignacion.AplicaMora);
      queryParameters.Add("@TasaInteres", asignacion.TasaInteres);
      queryParameters.Add("@PlazoMeses", asignacion.Plazo);
      queryParameters.Add("@Observaciones", asignacion.Observaciones);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_AsignacionCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public async Task AnularAsignacion(AnularAsignacionRequest request)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAsignacion", request.IdAsignacion);
      queryParameters.Add("@Observaciones", request.Observaciones);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_AsignacionAnular", queryParameters);

    }

    public async Task<List<EstadoCuenta>> GenerarEstadoCuenta(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAsignacion", id);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteReader<EstadoCuenta>("SP_EstadoCuentaGenerar", queryParameters);
      return result;
    }

    public async Task ActivarAsignacion(ActivarRequest request)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAsignacion", request.IdAsignacion);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_AsignacionActivar", queryParameters);
    }
   
  }
}
