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

namespace Dapper.Infrastructure.Repository
{
  public class AsignacionesRepository : GenericDapperRepository<Asignaciones>, IAsignacionesRepository
  {
    private readonly IUserAccesor _userAccesor;
    private readonly PayLotsDBContext _context;
    public AsignacionesRepository(IConfiguration configuration, IUserAccesor userAccesor, PayLotsDBContext context) : base(configuration, "Asignaciones", "IdAsignacion",context)
    {
      _userAccesor = userAccesor;
      _context = context;
    }

    public async Task<List<ViewAsignacionesLotes>> GetAsignacionesBeneficiario(int id)
    {
      //var result = _context.Set<ViewAsignacionesLotes>().Where(p => p.IdBeneficiario == id);
      var result = await _context.ViewAsignacionesLotes.Where(p => p.IdBeneficiario == id).ToListAsync();
      return result;
    }

    public override async Task<int> AddUpdateAsync(Asignaciones asignacion)
    {
      string user = _userAccesor.GetCurrentUser();
      var queryParameters = new DynamicParameters();
      queryParameters.Add("@IdAsignacion", asignacion.IdAsignacion,dbType: DbType.Int32);
      queryParameters.Add("@IdBeneficiario", asignacion.IdBeneficiario);
      queryParameters.Add("@IdLote",asignacion.IdLote);
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
      queryParameters.Add("@IdentityUser", "SPTest");
      queryParameters.Add("@UUA", user);

      var result = await ExecuteSP("SP_AsignacionCrearActualizar",queryParameters);
      if(result!=null) return (int)result; else return 0;

      

    }
  }
}
