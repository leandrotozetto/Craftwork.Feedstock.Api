using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using System;

namespace Craftwork.Feedstock.Api.Domain.Entities.Identifiers
{
    public class MeasureId : Identifier
    {
        public static MeasureId _empty;

        public static MeasureId Empty
        {
            get
            {
                _empty ??= new MeasureId(Guid.Empty);

                return _empty;

            }
        }

        private MeasureId(Guid value) : base(value) { }

        public static MeasureId New()
        {
            return new MeasureId(Guid.NewGuid());
        }

        public static MeasureId New(Guid value)
        {
            Ensure.That(value, nameof(value)).IdNotEmpty();

            return new MeasureId(value);
        }
    }
}