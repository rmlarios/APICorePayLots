using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Dapper.Core.Model;
using Dapper.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;

namespace Dapper.Infrastructure.Repository
{
  public class ProformaRepository : GenericDapperRepository<Proformas>, IProformaRepository
  {
    private readonly IUserAccesor _userAccesor;
    public ProformaRepository(IConfiguration configuration, PayLotsDBContext context,IUserAccesor userAccesor) : base(configuration, context)
    {
        _userAccesor = userAccesor;
    }

    public override async Task<int> AddUpdateAsync(int id,Proformas proforma)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdProforma", id, dbType: DbType.Int32);
      queryParameters.Add("@Nombre", proforma.Nombre);
      queryParameters.Add("@Domicilio", proforma.Domicilio);
      queryParameters.Add("@Telefono", proforma.Telefono);
      queryParameters.Add("@Email", proforma.Email);
      queryParameters.Add("@Proyecto", proforma.Proyecto);
      queryParameters.Add("@Lote", proforma.Lote);
      queryParameters.Add("@Area", proforma.Area);
      queryParameters.Add("@PrecioVara", proforma.PrecioVara);
      queryParameters.Add("@Prima", proforma.Prima);
      queryParameters.Add("@Interes", proforma.Interes);
      queryParameters.Add("@Plazo", proforma.Plazo);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_ProformaCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;
    }
  
  public async Task<List<Proformas>> GenerarProforma(int id)
    {      
      var result = await ExecuteReader<Proformas>("SELECT * FROM Proformas Where IdProforma=" + id,new DynamicParameters(),CommandType.Text);
      return result;
    }    
  }
}
