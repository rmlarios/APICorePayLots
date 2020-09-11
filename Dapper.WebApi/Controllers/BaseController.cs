using System.Data;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using Dapper.WebApi.Models;

namespace Dapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        protected IGenericDapperRepository<T> _repo;
        public BaseController(IGenericDapperRepository<T> repo)
        {
            _repo=repo;
        }

        // GET api/{T}/Listar
        [HttpGet("Listar")]
        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

         // GET api/{T}/5
        [HttpGet("{id}")]
        public async Task<T> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        
      /*  // POST api/base
        [HttpPost("")]
        public void Poststring(string value)
        {
        }

        // PUT api/base/5
        [HttpPut("{id}")]
        public void Putstring(int id, string value)
        {
        }

        // DELETE api/base/5
        [HttpDelete("{id}")]
        public void DeletestringById(int id)
        {
        } */
    }
}