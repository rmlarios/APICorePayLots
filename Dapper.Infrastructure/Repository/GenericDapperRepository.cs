using System;
using System.Runtime.Intrinsics.X86;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Application.Exceptions;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Microsoft.Extensions.Configuration;
using Dapper.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Dapper.Core.Model;

namespace Dapper.Infrastructure.Repository
{
  public class GenericDapperRepository<T> : IGenericDapperRepository<T> where T : class
  {
    public readonly string _connectionstring;
    private readonly string _tableName;
    private readonly string _idfieldname;

    private readonly PayLotsDBContext _context;


    public GenericDapperRepository(IConfiguration configuration, string TableName, string IDFieldName, PayLotsDBContext context)
    {
      _connectionstring = configuration.GetConnectionString("PayLotsConnectionString");
      _tableName = TableName;
      _idfieldname = IDFieldName;
      _context = context;

    }

    public virtual Task<int> AddUpdateAsync(T entity)
    {
      throw new System.NotImplementedException();
    }

    public virtual Task<bool> DeleteAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
      using (var connection = new SqlConnection(_connectionstring))
      {
        connection.Open();
        var result = await connection.QueryAsync<T>("SELECT * FROM " + _tableName);
        return result.ToList();
      }
    }

    public async Task<T> GetByIdAsync(int id)
    {

      using (var connection = new SqlConnection(_connectionstring))
      {
        connection.Open();
        var entity = await connection.QuerySingleOrDefaultAsync<T>("SELECT *  FROM " + _tableName + " WHERE " + _idfieldname + "=@ID", new { ID = id });

        if (entity == null)
          throw new ApiException("Registro no encontrado");

        return entity;

      }
    }
    #region :::::::::UTILIDADES
    /// <summary>
    /// Funcion que permite ejecutar un procedimiento almacenado como Escalar
    /// </summary>
    /// <param name="query">Nombre del procedimiento almacenado</param>
    /// <param name="paramet">Parametros del procedimiento almacenado</param>
    /// <returns>Retorna un Escalar obtenido de procedimiento almacenado</returns>
    public async Task<object> ExecuteSP(string query, object paramet)
    {
      using (var conn = new SqlConnection(_connectionstring))
      {
        var p = new DynamicParameters();

        await conn.OpenAsync();
        var result = await conn.ExecuteScalarAsync(sql: query, param: paramet, commandTimeout: 0, commandType: CommandType.StoredProcedure);


        return result;
      }
      //var result =  await _context.Database.ExecuteSqlRawAsync(sql:query,parameters:paramet);
      //return result;
    }
    /// <summary>
    /// Generar un identificador unico para pasarlo como parámetro a un procedimiento almacenado
    /// </summary>
    /// <param name="user">Usuario logueado</param>
    /// <returns>Retorna una cadena de texto</returns>
    public string GenerarIdentidad(string user)
    {
      return user + System.DateTime.Now.Date.ToShortDateString() + System.DateTime.Now.ToString("HH:mm:ss");
    }
    /// <summary>
    /// Consulta la tabla ErrorSQL para verificar si se produjo un error dentro un procedimiento almacenado
    /// </summary>
    /// <param name="IdentityUser">Identificador unico pasado como parametro a un procedimiento almacenado</param>
    /// <returns></returns>
    public string ObtenerErrorSQL(string IdentityUser)
    {
      var error = _context.Set<ErrorSql>().Where(p => p.IdentityUser == IdentityUser).Select(s => new { s.ErrorSql1 });
      return error.ToString();
    }
    #endregion

  }
}