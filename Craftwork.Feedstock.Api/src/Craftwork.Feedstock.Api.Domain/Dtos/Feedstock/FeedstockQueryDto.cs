using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;

namespace Craftwork.Feedstock.Api.Domain.Dtos.Feedstock
{
    public class FeedstockQueryDto
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
        /// Color`s name
        /// </summary>
        public string ColorName { get; private set; }

        /// <summary>
        /// Measure`s name.
        /// </summary>
        public string MeasureName { get; private set; }

        private FeedstockQueryDto() { }

        public static FeedstockQueryDto New(string name, Status status, string colorName, string measureName)
        {
            return new FeedstockQueryDto
            {
                Name = name,
                Status = status,
                MeasureName = measureName,
                ColorName = colorName
            };
        }
    }
}
