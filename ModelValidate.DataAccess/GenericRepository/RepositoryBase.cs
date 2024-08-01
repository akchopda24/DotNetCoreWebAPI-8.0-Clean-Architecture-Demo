using Microsoft.EntityFrameworkCore;
using ModelValidate.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.DataAccess.GenericRepository
{
    /// <summary>
    /// Generic repository service class for Insert, Update, Delete, Select operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ValidateMessageDbContext _dbContext;

        public RepositoryBase(ValidateMessageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get All datas from model
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<T>> GetAllAsync()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Get model by Id
        /// </summary>
        /// <param name="id">integer id</param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Add valid model data
        /// </summary>
        /// <param name="entity">entity as DB model which want to insert in DB</param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Update valid model data
        /// </summary>
        /// <param name="entity">entity as DB model which want to update in DB</param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete model data
        /// </summary>
        /// <param name="entity">entity as DB model which want to delete from DB</param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

    }
}
