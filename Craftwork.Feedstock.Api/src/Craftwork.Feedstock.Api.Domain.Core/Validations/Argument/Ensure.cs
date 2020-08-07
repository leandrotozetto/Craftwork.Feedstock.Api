using System;

namespace Craftwork.Feedstock.Api.Domain.Core.Validations.Argument
{
    /// <summary>
    /// Ensure that arguments are valids.
    /// </summary>
    public class Ensure
    {

        /// <summary>
        /// Creates a new Param <see cref="Param{T}"/>
        /// </summary>
        /// <typeparam name="T">Argument's type.</typeparam>
        /// <param name="value">Value will be check.</param>
        /// <param name="name">Argument's name.</param>
        /// <param name="customException">Custom exception.</param>
        /// <returns></returns>
        public static Param<T> That<T>(T value, string name, Action customException = null) => new Param<T>(value, name, customException);
    }
}
