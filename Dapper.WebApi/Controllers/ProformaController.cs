using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProformaController : BaseController<Proformas>
  {
    private readonly IProformaRepository _repository;
    public ProformaController(IProformaRepository repository) : base(repository)
    {
      _repository = repository;
    }    

  }
}