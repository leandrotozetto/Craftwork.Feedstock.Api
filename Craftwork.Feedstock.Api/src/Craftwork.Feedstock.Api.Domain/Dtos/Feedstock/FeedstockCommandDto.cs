using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using System;

namespace Craftwork.Feedstock.Api.Domain.Dtos.Feedstock
{
    public class FeedstockCommandDto
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

        /// <summary>
        /// Id of measure.
        /// </summary>
        public Guid MeasureId { get; private set; }

        /// <summary>
        /// Id of color.
        /// </summary>
        public Guid ColorId { get; private set; }

        /// <summary>
        /// Value of stock
        /// </summary>
        public int Stock { get; private set; }

        public static FeedstockCommandDto New(string name, Status status, Guid measureId, 
            int stock, Guid colorId, Guid tenantId)
        {
            return new FeedstockCommandDto
            {
                Name = name,
                Status = status,
                TenantId = tenantId,
                MeasureId = measureId,
                Stock = stock,
                ColorId = colorId
            };
        }
    }
}
