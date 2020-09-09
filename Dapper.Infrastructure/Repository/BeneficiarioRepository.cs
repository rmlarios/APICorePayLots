using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Dapper.Core.Model;
using Dapper.Infrastructure.Contexts;
//using Dapper.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Dapper.Infrastructure.Repository
{
  public class BeneficiarioRepository : GenericDapperRepository<Beneficiarios>, IBeneficiarioRepository
  {
    private readonly IUserAccesor _userAccesor;
    private readonly PayLotsDBContext _context;

    public BeneficiarioRepository(IConfiguration configuration, IUserAccesor userAccesor, PayLotsDBContext context) : base(configuration, "Beneficiarios", "IdBeneficiario", context)
    {
      _userAccesor = userAccesor;
      _context = context;

    }

    public override async Task<int> AddUpdateAsync(Beneficiarios beneficiario)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdBeneficiario", beneficiario.IdBeneficiario);
      queryParameters.Add("@NombreCompleto", beneficiario.NombreCompleto);
      queryParameters.Add("@CedulaIdentidad", beneficiario.CedulaIdentidad);
      queryParameters.Add("@Direccion", beneficiario.Direccion);
      queryParameters.Add("@Telefono", beneficiario.Telefono);
      queryParameters.Add("@IdentityUser", "DapperAtempUpdate");
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_BeneficiarioCrearActualizar", queryParameters);
      if (result != null) return (int)result; else return 0;

      /* var affectedRows = await conn.ExecuteScalarAsync(
                  sql: "SP_BeneficiarioCrearActualizar",
                  param: queryParameters,
                  commandType: CommandType.StoredProcedure);
    return (int)affectedRows; */
    }
    public override async Task<bool> DeleteAsync(int id)
    {
      string user = _userAccesor.GetCurrentUser();
      var IdentityUser = GenerarIdentidad(user);
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdBeneficiario", id);
      queryParameters.Add("@UUA", user);
      queryParameters.Add("@IdentityUser", IdentityUser);
      var result = await ExecuteSP("SP_BeneficiarioEliminar", queryParameters);
      if (ObtenerErrorSQL(IdentityUser) != "")
        return false;
      else
        return true;

    }

  }


}
