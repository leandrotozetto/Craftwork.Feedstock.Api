using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface that defines the repository.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// Can be used to linq query.
        /// </summary>
        DbSet<T> DbSet { get; }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <param name="filter">Filter to define user to be returned.</param>
        /// <param name="orderBy">Order users.</param>
        /// <param name="includes">Entities related.</param>
        /// <param name="page">Current page.</param>
        /// <param name="qtyPerPage">Quantity of registers per page.</param>
        /// <returns>Returns a list of users.</returns>
        Task<IPagination<T>> ListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, object>> includes = null, int page = 0, int qtyPerPage = 0);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <param name="filter">Filter to define entities to be returned.</param>
        /// <param name="orderBy">Columns name to order users.</param>
        /// <param name="page">Current page.</param>
        /// <param name="qtyPerPage">Quantity of registers per page.</param>
        /// <returns>Returns a list of entities.</returns>
        Task<IPagination<T>> ListAsync(Expression<Func<T, bool>> filter = null, string orderBy = null, int page = 0, int qtyPerPage = 0);

        ///// <summary>
        ///// Get user by id.
        ///// </summary>
        ///// <param name="keyValues">The values of the primary key for the entity to be found..</param>
        ///// <returns>Returns user found.</returns>
        //Task<T> GetAsync(params object[] keyValues);

        /// <summary>
        /// Get user by filter.
        /// </summary>
        /// <param name="filter">Filter to define entity to be returned.</param>
        /// <returns>Returns entity found.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">Entity to be created.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entities">Entity to be created.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        Task BulkInsertAsync(IEnumerable<T> entities);

        ///// <summary>        
        ///// Deletes a entity.
        ///// </summary>
        ///// <param name="keyValues">The values of the primary key for the entity to be found..</param>
        ///// <returns>Returns quantity of entities affected.</returns>
        //Task DeleteAsync(params object[] keyValues);

        /// <summary>
        /// Deletes the entities.
        /// </summary>
        /// <param name="entities">Entities to be deleted.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        Task BulkDeleteAsync(IEnumerable<T> entities);

        /// <summary>
        /// Updates a entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes a entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        void Delete(T entity);

        /// <summary>
        /// Updates the entities.
        /// </summary>
        /// <param name="entities">Entities to be updated.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        Task BulkUpdateAsync(IEnumerable<T> entities);
    }
}
