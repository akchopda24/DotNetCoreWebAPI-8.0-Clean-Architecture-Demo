using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.DataAccess.GenericRepository
{
    /// <summary>
    /// Generic repository interface for Insert, Update, Delete, Select operation
    /// </summary>
    /// <typeparam name="T">Entity Model</typeparam>
    public interface IRepositoryBase<T>
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T Entity);
        Task UpdateAsync(T Entity);
        Task DeleteAsync(T entity);
    }
}
