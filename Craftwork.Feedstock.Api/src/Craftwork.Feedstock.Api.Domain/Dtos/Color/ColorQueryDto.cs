using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;

namespace Craftwork.Feedstock.Api.Domain.Dtos.Color
{
    public class ColorQueryDto : IQueryDto
    {
        public static readonly ColorQueryDto Empty;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool Status { get; private set; }

        private ColorQueryDto() { }

        static ColorQueryDto()
        {
            Empty ??= new ColorQueryDto();
        }

        public static ColorQueryDto New(string name, bool status)
        {
            return new ColorQueryDto
            {
                Name = name,
                Status = status
            };
        }

        public bool IsEmpty()
        {
            return Equals(Empty);
        }
    }
}
