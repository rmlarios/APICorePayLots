using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Exceptions;
using Dapper.Application.Interfaces;
using Dapper.Application.Wrappers;
using Dapper.Core.Model;
//using Dapper.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BeneficiarioController : BaseController<Beneficiarios>
  {
    private readonly IBeneficiarioRepository _repository;
    public BeneficiarioController(IBeneficiarioRepository beneficiarioRepository) : base(beneficiarioRepository)
    {
      _repository = beneficiarioRepository;
    }
    
  }
}