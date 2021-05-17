using System;
using System.Collections.Generic;
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
    public class AbonosPrimaRepository : GenericDapperRepository<AbonosPrima>, IAbonosPrimaRepository
    {
        private readonly IUserAccesor _userAccesor;
    public AbonosPrimaRepository(IConfiguration configuration, PayLotsDBContext context,IUserAccesor userAccesor) : base(configuration, context)
    {
        _userAccesor = userAccesor;
    }


public override async Task<int> AddUpdateAsync(int id,AbonosPrima prima)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAbonoPrima", id, dbType: DbType.Int32);
      queryParameters.Add("@IdAsignacion", prima.IdAsignacion, dbType: DbType.Int32);
      queryParameters.Add("@Monto", prima.Monto, dbType: DbType.Decimal);
      queryParameters.Add("@Fecha", prima.Fecha, dbType: DbType.DateTime);      
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_AbonosPrimaCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

    }

    public override async Task<bool> DeleteAsync(int id){
    string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAbonoPrima", id);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteSP("SP_AbonoPrimaEliminar", queryParameters);
      return true;
    }

    public async Task<List<TicketPrima>> GenerarTicketPrima(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAbonoPrima", id);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      var result = await ExecuteReader<TicketPrima>("SP_TicketPrimaGenerar", queryParameters);
      return result;
    }
  }
}
