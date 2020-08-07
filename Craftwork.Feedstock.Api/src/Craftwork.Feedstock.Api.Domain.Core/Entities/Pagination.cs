using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Craftwork.Feedstock.Api.Domain.Core.Entities
{
    /// <summary>
    /// Return a list of entities with informations of the pagetion.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    [Serializable]
    public class Pagination<T> : IPagination<T>
        where T : class
    {
        /// <summary>
        /// List of the entities.
        /// </summary>
        public IEnumerable<T> Entities { get; private set; }

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Total of pages.
        /// </summary>
        public int TotalPages => Count == 0 || Entities == null ? 0 : Count % ItemsPerPage == 0 ? Count / ItemsPerPage : (Count / ItemsPerPage) + 1;

        /// <summary>
        /// Total quantity of entities.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Quantity of items per page.
        /// </summary>
        public int ItemsPerPage { get; private set; }

        /// <summary>
        /// Create a pagetion object.
        /// </summary>
        private Pagination() { }

        /// <summary>
        /// Create a pagetion object.
        /// </summary>
        /// <param name="entities">Entity will be display.</param>
        /// <param name="totalPages">Total of entity.</param>
        /// <param name="itemsPerPage">Quantity of register per page.</param>
        /// <param name="currentPage">Current page.</param>
        public static IPagination<T> New(IEnumerable<T> entities, int totalPages, int itemsPerPage, int currentPage)
        {
            Ensure.That(totalPages, nameof(totalPages)).HasValue();
            Ensure.That(currentPage, nameof(currentPage)).HasValue();
            Ensure.That(itemsPerPage, nameof(itemsPerPage)).HasValue();
            Ensure.That(entities, nameof(entities)).EntityIsNotNull();

            return new Pagination<T>
            {
                Count = totalPages,
                ItemsPerPage = itemsPerPage,
                Entities = entities,
                CurrentPage = currentPage
            };
        }

        /// <summary>
        /// Create a pagetion object.
        /// </summary>
        /// <param name="entities">Entity will be display.</param>
        /// <param name="total">Total of entity.</param>
        /// <param name="itemsPerPage">Quantity of register per page.</param>
        /// <param name="page">Current page.</param>
        public static IPagination<T> Empty => new Pagination<T>
        {
            Count = 0,
            ItemsPerPage = 0,
            Entities = Enumerable.Empty<T>(),
            CurrentPage = 0
        };

        public bool IsEmpty()
        {
            return !Entities.Any();
        }

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}