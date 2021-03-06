using System.Runtime.CompilerServices;
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
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Dapper.Infrastructure.Repository
{
  public class GenericDapperRepository<T> : IGenericDapperRepository<T> where T : class
  {
    public readonly string _connectionstring;
    private readonly PayLotsDBContext _context;


    public GenericDapperRepository(IConfiguration configuration, PayLotsDBContext context)
    {
      _connectionstring = configuration.GetConnectionString("PayLotsConnectionString");
      _context = context;

    }

    public virtual Task<int> AddUpdateAsync(int id, T entity)
    {
      throw new System.NotImplementedException();
    }

    public virtual Task<bool> DeleteAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<List<T>> GetAllAsync()
    {
      return await _context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(int t, int s)
    {
      return await _context.Set<T>().Skip(s).Take(t).ToListAsync();
    }

    public async Task<List<M>> GetAllData<M>() where M : class
    {
      return await _context.Set<M>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
      //return await _context.Set<T>().FindAsync(id);
      var result = await _context.Set<T>().FindAsync(id);
      if (result == null)
        throw new ApiException("Registro no encontrado");
      return result;


      /* using (var connection = new SqlConnection(_connectionstring))
      {
        connection.Open();
        var entity = await connection.QuerySingleOrDefaultAsync<T>("SELECT *  FROM " + _tableName + " WHERE " + _idfieldname + "=@ID", new { ID = id });

        if (entity == null)
          throw new ApiException("Registro no encontrado");

        return entity;

      } */
    }

    public async Task<List<M>> FindAsync<M>(Expression<Func<M, bool>> expression = null) where M : class
    {
      if (expression != null)
        return await _context.Set<M>().Where(expression).ToListAsync();
      else
        return await _context.Set<M>().ToListAsync();
    }

    public async Task<List<M>> FindAsync<M>(int t, int s, string sort=null, Expression<Func<M, bool>> expression = null) where M : class
    {
      if (sort != null && sort != "")
      {
        if (expression != null)
          return await _context.Set<M>().Where(expression).OrderByDescending(CreateExpression<M>(sort)).Skip(s).Take(t).ToListAsync();
        else
          return await _context.Set<M>().OrderByDescending(CreateExpression<M>(sort)).Skip(s).Take(t).ToListAsync();
      }
      else
      {
        if (expression != null)
          return await _context.Set<M>().Where(expression).Skip(s).Take(t).ToListAsync();
        else
          return await _context.Set<M>().Skip(s).Take(t).ToListAsync();
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
        await conn.OpenAsync();
        var result = await conn.ExecuteScalarAsync(sql: query, param: paramet, commandTimeout: 0, commandType: CommandType.StoredProcedure);

        string IdentityUser = ((DynamicParameters)paramet).Get<string>("IdentityUser");
        string ErrorSql = ObtenerErrorSQL(IdentityUser);
        if (ErrorSql != "")
          throw new ApiException(ErrorSql);


        return result;
      }
      //var result =  await _context.Database.ExecuteSqlRawAsync(sql:query,parameters:paramet);
      //return result;
    }


    public async Task<List<M>> ExecuteReader<M>(string query, object paramet, CommandType type = CommandType.StoredProcedure) where M : class
    {
      using (var conn = new SqlConnection(_connectionstring))
      {
        await conn.OpenAsync();
        //List<M> result = new List<M>();
        var result = await conn.QueryAsync<M>(query, paramet, commandTimeout: 0, commandType: type);

        //var result = await conn.ExecuteReaderAsync(sql: query,param: paramet,commandTimeout:0,commandType: CommandType.StoredProcedure);
        //var result = await conn.ExecuteScalarAsync(sql: query, param: paramet, commandTimeout: 0, commandType: CommandType.StoredProcedure);
        if (((DynamicParameters)paramet).ParameterNames.Count() != 0)
        {
          string IdentityUser = ((DynamicParameters)paramet).Get<string>("IdentityUser");
          string ErrorSql = ObtenerErrorSQL(IdentityUser);
          if (ErrorSql != "")
            throw new ApiException(ErrorSql);
        }

        return result.ToList();
      }
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
      //var error = _context.Set<ErrorSql>().Where(p => p.IdentityUser == IdentityUser).Select(s => new { s.ErrorSql1 });
      var error = _context.Set<ErrorSql>().Where(p => p.IdentityUser == IdentityUser).Select(s => s.ErrorSql1).FirstOrDefault();
      return error == null ? "" : error.ToString();
    }

    public async Task<string> Filter(string query)
    {
      SqlConnection conn = new SqlConnection(_connectionstring);

      using (SqlCommand cmd = new SqlCommand(query, conn))
      {
        cmd.CommandType = CommandType.Text;
        //cmd.Parameters.Add(paramet);
        //cmd.Parameters.AddRange(parameters);

        conn.Open();
        // When using CommandBehavior.CloseConnection, the connection will be closed when the   
        // IDataReader is closed.  
        SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        var dt = new DataTable();
        dt.Load(reader);
        //return dt;
        return JsonConvert.SerializeObject(dt);

      }
    }

    public async Task<int> GetCount<M>(Expression<Func<M, bool>> expression = null) where M : class
    {
      if (expression == null)
        return await _context.Set<M>().CountAsync();
      else
        return await _context.Set<M>().Where(expression).CountAsync();

    }

    static Expression<Func<M, object>> CreateExpression<M>(string propertyName)
    {
      var type = typeof(M);
      var property = type.GetProperty(propertyName);
      var parameter = Expression.Parameter(type);
      var access = Expression.Property(parameter, property);
      var convert = Expression.Convert(access, typeof(object));
      var function = Expression.Lambda<Func<M, object>>(convert, parameter);

      return function;
    }
    #endregion

  }
}