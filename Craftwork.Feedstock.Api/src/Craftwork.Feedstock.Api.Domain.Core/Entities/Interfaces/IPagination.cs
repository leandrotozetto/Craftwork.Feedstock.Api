using System;
using System.Collections.Generic;

namespace Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces
{
    /// <summary>
    /// Interface that defines a pagination entity of the domain layer.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public interface IPagination<T> : IDisposable
    {
        /// <summary>
        /// List of entities.
        /// </summary>
        IEnumerable<T> Entities { get; }

        /// <summary>
        /// Total quantity of entities.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Current page.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Total of pages.
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Quantity of items per page.
        /// </summary>
        int ItemsPerPage { get; }

        /// <summary>
        /// It checks if this object is empty. 
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}
