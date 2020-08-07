using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Core.Exceptions;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Infrastructure.Repositories
{
    /// <summary>
    /// Defines the actions of the repository.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public class Repository<T> : IRepository<T>
         where T : class
    {
        protected DbContext Context;

        /// <summary>
        /// Can be used to linq query.
        /// </summary>
        public DbSet<T> DbSet { get; private set; }

        /// <summary>
        /// Creates a new instance of Repository <see cref="Repository"/>.
        /// </summary>
        /// <param name="context">Context for queries in db.</param>
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">Entity to be created.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                Ensure.That(entity, nameof(entity)).EntityIsNotNull();
            }

            try
            {
                await Context.AddAsync(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entities">Entity to be created.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task BulkInsertAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException(nameof(entities));
            }

            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;

                await Context.AddRangeAsync(entities);

                //    await Context.BulkInsertAsync(entities as IList<T>, new BulkConfig { BatchSize = 4000 });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                if (entity == null)
                {
                    throw new ArgumentException(nameof(entity));
                }

                try
                {
                    Context.Update(entity);
                    //Context.Entry<T>(entity).State = EntityState.Modified;
                }
                catch
                {
                    throw;
                }
            });
        }

        /// <summary>
        /// Updates the entities.
        /// </summary>
        /// <param name="entities">Entities to be updated.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task BulkUpdateAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException(nameof(entities));
            }

            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                await Context.AddRangeAsync(entities);
                //  await Context.BulkUpdateAsync(entities as IList<T>, new BulkConfig { BatchSize = 4000 });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }

            try
            {
                Context.Remove(entity);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Deletes the entities.
        /// </summary>
        /// <param name="entities">Entities to be deleted.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task BulkDeleteAsync(IEnumerable<T> entities)
        {
            await Task.Run(() =>
            {
                if (entities == null)
                {
                    throw new ArgumentException(nameof(entities));
                }

                try
                {
                    Context.ChangeTracker.AutoDetectChangesEnabled = false;

                    Context.RemoveRange(entities);
                    //await Context.BulkDeleteAsync(entities as IList<T>, new BulkConfig { BatchSize = 4000 });
                }
                catch
                {
                    throw;
                }
            });
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <param name="filter">Filter to define entities to be returned.</param>
        /// <param name="orderBy">Columns name to order users.</param>
        /// <param name="page">Current page.</param>
        /// <param name="qtyPerPage">Quantity of registers per page.</param>
        /// <returns>Returns a list of entities.</returns>
        public async Task<IPagination<T>> ListAsync(Expression<Func<T, bool>> filter = null, string orderBy = null,
            int page = 0, int qtyPerPage = 0)
        {
            var orderByFunc = GetOrderByAsync(orderBy);

            return await ListAsync(filter, orderByFunc, null, page, qtyPerPage);
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <param name="filter">Filter to define user to be returned.</param>
        /// <param name="orderBy">Order users.</param>
        /// <param name="includes">Entities related.</param>
        /// <param name="page">Current page.</param>
        /// <param name="qtyPerPage">Quantity of registers per page.</param>
        /// <returns>Returns a list of users.</returns>
        public async Task<IPagination<T>> ListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            Expression<Func<T, object>> includes = null, int page = 0, int qtyPerPage = 0)
        {
            try
            {
                using var transaction = await Context.Database.BeginTransactionAsync();
                var query = filter != null ? DbSet.AsNoTracking().Where(filter) : DbSet.AsNoTracking().AsQueryable();

                if (includes != null)
                {
                    query.Include(includes);
                }

                if (orderby != null)
                {
                    query = orderby(query);
                }

                var total = query.Count();
                var qtd = qtyPerPage == 0 ? 1 : qtyPerPage;
                var skip = page == 1 ? 0 : (page - 1) * qtyPerPage;

                if (total == 0)
                {
                    //throw new EntityNotFoundException("Não foram encontrados registros para o termo pesquisado");
                }

                if (qtyPerPage == 0)
                {
                    qtyPerPage = total;
                }

                var result = await query.Skip(skip).Take(qtyPerPage).ToListAsync();

                transaction.Commit();

                return Pagination<T>.New(result, total, qtyPerPage, page);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        ///// <summary>
        ///// Get user by id.
        ///// </summary>
        ///// <param name="id">User id.</param>
        ///// <returns>Returns user found.</returns>
        //public async Task<T> GetAsync(Guid id)
        //{
        //    try
        //    {
        //        return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// Get user by filter.
        /// </summary>
        /// <param name="filter">Filter to define entity to be returned.</param>
        /// <returns>Returns entity found.</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(filter);
            }
            catch
            {
                throw;
            }
        }

        private static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderByAsync(string orderColumn, string orderType = null)
        {
            if (string.IsNullOrWhiteSpace(orderColumn))
            {
                return null;
            }

            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumn.ToLower().Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            var hasProp = type.GetProperties().FirstOrDefault(x => props.Contains(x.Name.ToLower()) == true);

            if (hasProp == null)
            {
                throw new DomainException(orderColumn, "O nome da coluna utilizado para ordenação é inválido");
            }

            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }

            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType == "asc" || string.IsNullOrWhiteSpace(orderType) ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);

            return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        }

        private bool disposedValue = false;

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
