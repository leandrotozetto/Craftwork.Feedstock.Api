using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Exceptions;
using System;

namespace Craftwork.Feedstock.Api.Domain.Core.Validations.Argument
{
    /// <summary>
    /// Extensions that contains the methods to valid the arguments.
    /// </summary>
    public static class ParamExtensions
    {
        /// <summary>
        /// Checks if entity is not null.
        /// </summary>
        /// <typeparam name="T">argument's type.</typeparam>
        /// <param name="param">Data that will be validated.</param>
        public static void EntityIsNotNull<T>(this Param<T> param)
        {
            if (param._value == null)
            {
                if (param._customException != null)
                {
                    param._customException.Invoke();
                }

                throw new DomainException(param._name, DomainMessage.EntityIsNull(param._name));
            }
        }

        /// <summary>
        /// Checks if arg is not null.
        /// </summary>
        /// <typeparam name="T">argument's type.</typeparam>
        /// <param name="param">Data that will be validated.</param>
        public static void ArgumentIsNotNull<T>(this Param<T> param)
        {
            if (param._value == null)
            {
                if (param._customException != null)
                {
                    param._customException.Invoke();
                }

                throw new ArgumentNullException(nameof(param._name));
            }
        }

        /// <summary>
        /// Checks if arg is not null.
        /// </summary>
        /// <param name="param">Data that will be validated.</param>
        public static void IsNullOrWhiteSpace(this Param<string> param)
        {
            if (string.IsNullOrWhiteSpace(param._value))
            {
                if (param._customException != null)
                {
                    param._customException.Invoke();
                }

                throw new ArgumentException(DomainMessage.IsNullOrWhiteSpace(nameof(param._name)));
            }
        }

        /// <summary>
        /// Checks is the entity is not null.
        /// </summary>
        /// <typeparam name="T">argument's type.</typeparam>
        /// <param name="param">Data that will be validated.</param>
        public static void EntityExists<T>(this Param<T> param)
        {
            if (param._value == null)
            {
                if (param._customException != null)
                {
                    param._customException.Invoke();
                }

                throw new Exception(DomainMessage.IdNotExist(param._name));
            }
        }

        /// <summary>
        /// Checks if the Guid is not empty
        /// </summary>
        /// <param name="param">Data that will be validated.</param>
        public static ArgumentValidationResult IdNotEmpty(this Param<Guid> param)
        {
            if (param._value == Guid.Empty)
            {
                var domainError = DomainError.New(param._name, DomainMessage.IdNotExist(param._name));

                return ArgumentValidationResult.New(domainError);
            }

            return ArgumentValidationResult.Success;
        }

        /// <summary>
        /// Checks if the int is greather than zero
        /// </summary>
        /// <param name="param">Data that will be validated.</param>
        public static void HasValue(this Param<int> param)
        {
            if (param._value == 0)
            {
                if (param._customException != null)
                {
                    param._customException.Invoke();
                }

                throw new Exception(DomainMessage.IdNotExist(param._name));
            }
        }
    }
}
