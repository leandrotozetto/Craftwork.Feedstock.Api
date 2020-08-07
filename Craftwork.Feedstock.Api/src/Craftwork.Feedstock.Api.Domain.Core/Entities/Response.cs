using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.Exceptions;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Craftwork.Feedstock.Api.Domain.Core.Entities
{
    public class Response : IResponseError, IResponse
    {
        private readonly ICollection<DomainError> _errors;

        private Response()
        {
            _errors = new Collection<DomainError>();
        }

        /// <summary>
        /// Operation's status
        /// </summary>
        public ResponseStatus Status { get; private set; }

        private readonly static Response _success;

        /// <summary>
        /// Return data.
        /// </summary>
        public dynamic Data { get; private set; }

        public IResponse Result => this;

        static Response()
        {
            if (_success is null)
            {
                _success = new Response { Status = ResponseStatusEnum.Success };
            }
        }

        public static IResponseError ValidationError(DomainError domainError)
        {
            var response = new Response()
            {
                Status = ResponseStatusEnum.ValidationError,
            };

            response.AddError(domainError);

            return response;
        }

        public static IResponse FatalError(DomainError domainError)
        {
            var response = new Response()
            {
                Status = ResponseStatusEnum.FatalError,
            };

            response.AddError(domainError);

            return response;
        }

        public static IResponse Error()
        {
            var domainError = DomainError.New(string.Empty, "Não foi possível processar a requisição");
            var response = new Response()
            {
                Status = ResponseStatusEnum.FatalError,
            };

            response.AddError(domainError);

            return response;
        }

        public static IResponse Success(dynamic data)
        {
            return new Response()
            {
                Status = ResponseStatusEnum.Success,
                Data = data
            };
        }

        public static IResponse Success()
        {
            return _success;
        }

        /// <summary>
        /// Add error messages.
        /// </summary>
        /// <param name="domainError">Domain error.</param>
        public IResponseError AddError(DomainError domainError)
        {
            if (domainError != null && ResponseStatusEnum.IsErrorStatus(Status))
            {
                var error = _errors.FirstOrDefault(x => x.Property == domainError.Property);

                if (error != null)
                {
                    foreach (var message in domainError.Messages)
                    {
                        error.Messages.Add(message);
                    }

                    return this;
                }

                _errors.Add(domainError);
            }

            Data = _errors;

            return this;
        }
    }

    public interface IResponseError
    {
        IResponseError AddError(DomainError domainError);

        IResponse Result { get; }
    }

    public interface IResponse
    {
        /// <summary>
        /// Return data.
        /// </summary>
        dynamic Data { get; }

        /// <summary>
        /// Operation's status
        /// </summary>
        public ResponseStatus Status { get; }
    }
}
