using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;

namespace Craftwork.Feedstock.Api.Domain.Requests.Queries.Measure
{
    public class GetMeasureQueryRequest : IRequest<MeasureQueryDto>
    {
        public MeasureId MeasureId { get; private set; }

        private GetMeasureQueryRequest() { }

        public static GetMeasureQueryRequest New(MeasureId measureId)
        {
            return new GetMeasureQueryRequest
            {
                MeasureId = measureId
            };
        }
    }
}
