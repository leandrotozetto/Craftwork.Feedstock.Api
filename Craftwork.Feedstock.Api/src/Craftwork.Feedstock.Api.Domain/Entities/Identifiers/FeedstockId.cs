using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using System;

namespace Craftwork.Feedstock.Api.Domain.Entities.Identifiers
{
    public class FeedstockId : Identifier
    {
        public static FeedstockId _empty;

        public static FeedstockId Empty
        {
            get
            {
                _empty ??= new FeedstockId(Guid.Empty);

                return _empty;

            }
        }

        private FeedstockId(Guid value) : base(value) { }

        public static FeedstockId New()
        {
            return new FeedstockId(Guid.NewGuid());
        }

        public static FeedstockId New(Guid value)
        {
            Ensure.That(value, nameof(value)).IdNotEmpty();

            return new FeedstockId(value);
        }
    }
}