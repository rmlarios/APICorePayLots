using System;
using Dapper.Application.Interfaces;
using Dapper.Core.Model;

namespace Dapper.WebApi.Controllers
{
  public class MunicipiosController : BaseController<ViewDepartamentosMunicipios>
  {
    public MunicipiosController(IGenericDapperRepository<ViewDepartamentosMunicipios> repo) : base(repo)
    {
    }
  }
}
