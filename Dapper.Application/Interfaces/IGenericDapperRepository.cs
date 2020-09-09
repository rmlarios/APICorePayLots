using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Dapper.Application.Interfaces
{
    public interface IGenericDapperRepository<T> where T : class 
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> AddUpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

        Task<object> ExecuteSP(string sql, object parameters);
        
    }
}
