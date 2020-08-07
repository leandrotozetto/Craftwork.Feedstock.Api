using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;

namespace Craftwork.Feedstock.Api.Domain.Dtos.Measure
{
    public class MeasureQueryDto
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public Status Status { get; private set; }

        private MeasureQueryDto() { }

        public static MeasureQueryDto New(string name, Status status)
        {
            return new MeasureQueryDto
            {
                Name = name,
                Status = status
            };
        }
    }
}
