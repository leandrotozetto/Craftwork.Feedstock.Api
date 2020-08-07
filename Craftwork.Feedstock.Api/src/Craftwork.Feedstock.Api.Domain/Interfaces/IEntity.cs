using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;

namespace Craftwork.Feedstock.Api.Domain.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// Status of register.
        /// </summary>
        Status Status { get; }

        /// <summary>
        /// Date of create
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// Date of Update
        /// </summary>
        DateTime? UpdateDate { get; }

        /// <summary>
        /// Cliente Id
        /// </summary>
        TenantId TenantId { get; }
    }
}
