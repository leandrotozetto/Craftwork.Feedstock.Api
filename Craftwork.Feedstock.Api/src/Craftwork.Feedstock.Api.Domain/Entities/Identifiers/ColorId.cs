using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using System;

namespace Craftwork.Feedstock.Api.Domain.Entities.Identifiers
{
    public class ColorId : Identifier
    {
        public static ColorId _empty;

        public static ColorId Empty
        {
            get
            {
                _empty ??= new ColorId(Guid.Empty);

                return _empty;

            }
        }

        private ColorId(Guid value) : base(value) { }

        public static ColorId New()
        {
            return new ColorId(Guid.NewGuid());
        }

        public static ColorId New(Guid value)
        {
            Ensure.That(value, nameof(value)).IdNotEmpty();

            return new ColorId(value);
        }
    }
}