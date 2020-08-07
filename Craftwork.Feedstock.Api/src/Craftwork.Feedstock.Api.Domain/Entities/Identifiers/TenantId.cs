using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using System;

namespace Craftwork.Feedstock.Api.Domain.Entities.Identifiers
{
    public class TenantId : Identifier
    {
        public static TenantId _empty;

        public static TenantId Empty
        {
            get
            {
                _empty ??= new TenantId(Guid.Empty);

                return _empty;

            }
        }

        private TenantId(Guid value) : base(value) { }

        public static TenantId New()
        {
            return new TenantId(Guid.NewGuid());
        }

        public static TenantId New(Guid value)
        {
            Ensure.That(value, nameof(value)).IdNotEmpty();

            return new TenantId(value);
        }
    }
}