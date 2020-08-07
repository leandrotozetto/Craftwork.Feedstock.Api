using System;

namespace Craftwork.Feedstock.Api.Domain.Core.Validations.Argument
{
    /// <summary>
    /// Represents the values will be validated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Param<T>
    {
        /// <summary>
        /// Value that will be validated.
        /// </summary>
        public readonly T _value;

        /// <summary>
        /// Argument's name that will be validated.
        /// </summary>
        public readonly string _name;

        /// <summary>
        /// Custom exception
        /// </summary>
        public readonly Action _customException;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="value">Value that will be validated.</param>
        /// <param name="name">Argument's name that will be validated.</param>
        /// <param name="customException">Custom exception.</param>
        public Param(T value, string name, Action customException = null)
        {
            _value = value;
            _name = name;
            _customException = customException;
        }
    }
}
