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
  public class PagoRepository : GenericDapperRepository<Pagos>, IPagoRepository
  {
    private readonly IUserAccesor _userAccesor;
    public PagoRepository(IConfiguration configuration, PayLotsDBContext context,IUserAccesor userAccesor) : base(configuration, context)
    {
        _userAccesor = userAccesor;
    }

    public override async Task<int> AddUpdateAsync(Pagos pago)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdPago", pago.IdPago, dbType: DbType.Int32);
      queryParameters.Add("@IdAsignacion", pago.IdAsignacion, dbType: DbType.Int32);
      queryParameters.Add("@NumeroRecibo", pago.NumeroRecibo);
      queryParameters.Add("@FechaRecibo", pago.FechaRecibo);
      queryParameters.Add("@MesPagado", pago.MesPagado);
      queryParameters.Add("@Moneda", pago.Moneda);
      queryParameters.Add("@TasaCambio", pago.TasaCambio);
      queryParameters.Add("@MontoPago", pago.MontoPago);
      queryParameters.Add("@TotalPagado", pago.MontoEfectivo);
      queryParameters.Add("@Interes", pago.Interés);
      queryParameters.Add("@Mora", pago.Mora);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_PagoCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public override async Task<bool> DeleteAsync(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdPago", id);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_PagoEliminar", queryParameters);
      return true;
    }

    
  }
}
