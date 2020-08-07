using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using System;

namespace Craftwork.Feedstock.Api.Domain.Dtos.Measure
{
    public class MeasureCommandDto
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public Status Status { get; private set; }

        /// <summary>
        /// Cliente Id.
        /// </summary>
        public Guid TenantId { get; private set; }

        public static MeasureCommandDto New(string name, Status status, Guid tenantId)
        {
            return new MeasureCommandDto
            {
                Name = name,
                Status = status,
                TenantId = tenantId
            };
        }
    }
}
