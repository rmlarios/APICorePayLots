using System.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Dapper.Application.Interfaces
{
  public interface IGenericDapperRepository<T> where T : class
  {
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(int t, int s);
    Task<int> GetCount<M>(Expression<Func<M, bool>> expression = null) where M : class;
    Task<int> AddUpdateAsync(int id, T entity);
    Task<bool> DeleteAsync(int id);
    //Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task<List<M>> FindAsync<M>(Expression<Func<M, bool>> expression = null) where M : class;
    Task<List<M>> FindAsync<M>(int t, int s, string sort=null,Expression<Func<M, bool>> expression = null) where M : class;
    Task<object> ExecuteSP(string sql, object parameters);
    Task<string> Filter(string condition);
    Task<List<M>> GetAllData<M>() where M : class;
    
  }
}
