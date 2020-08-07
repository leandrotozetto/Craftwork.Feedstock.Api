using System;

namespace Craftwork.Feedstock.Api.Domain.Dtos.Color
{
    public class ColorCommandDto
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool Status { get; private set; }

        /// <summary>
        /// Cliente Id.
        /// </summary>
        public Guid ColorId { get; private set; }

        public static ColorCommandDto New(string name, bool status, Guid colorId)
        {
            return new ColorCommandDto
            {
                Name = name,
                Status = status,
                ColorId = colorId
            };
        }

        private ColorCommandDto() { }
    }
}
