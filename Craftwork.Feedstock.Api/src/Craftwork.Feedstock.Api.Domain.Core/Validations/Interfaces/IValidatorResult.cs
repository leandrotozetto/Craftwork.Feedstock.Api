using Craftwork.Feedstock.Api.Domain.Core.Exceptions;
using System.Collections.Generic;

namespace Craftwork.Feedstock.Api.Domain.Core.Validations.Interfaces
{
    public interface IValidatorResult
    {
        /// <summary>
        /// List with erros messages.
        /// </summary>
        IEnumerable<DomainError> Errors { get; }

        bool IsValid { get; }
    }
}
