using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;

namespace Craftwork.Feedstock.Api.Domain.Requests.Queries.Color
{
    public class GetColorQueryRequest : IRequest<ColorQueryDto>
    {
        public ColorId ColorId { get; private set; }

        private GetColorQueryRequest() { }

        public static GetColorQueryRequest New(ColorId colorId)
        {
            return new GetColorQueryRequest
            {
                ColorId = colorId
            };
        }
    }
}
