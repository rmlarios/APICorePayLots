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
  public class ProformaRepository : GenericDapperRepository<Proformas>, IProformaRepository
  {
    private readonly IUserAccesor _userAccesor;
    public ProformaRepository(IConfiguration configuration, PayLotsDBContext context,IUserAccesor userAccesor) : base(configuration, context)
    {
        _userAccesor = userAccesor;
    }

    public override async Task<int> AddUpdateAsync(Proformas proforma)
    {       
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdProforma", proforma.IdProforma, dbType: DbType.Int32);
      queryParameters.Add("@Nombre", proforma.Nombre);
      queryParameters.Add("@Domicilio", proforma.Domicilio);
      queryParameters.Add("@Telefono", proforma.Telefono);
      queryParameters.Add("@Email", proforma.Email);
      queryParameters.Add("@Proyecto", proforma.Proyecto);
      queryParameters.Add("@Lote", proforma.Lote);
      queryParameters.Add("@Area", proforma.Area);
      queryParameters.Add("@PrecioVara", proforma.PrecioVara);
      queryParameters.Add("@Prima", proforma.Interes);
      queryParameters.Add("@Interes", proforma.Interes);
      queryParameters.Add("@Plazo", proforma.Plazo);
      queryParameters.Add("@IdentityUser", GenerarIdentidad(user));
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_ProformaCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;
    }
  }
}
