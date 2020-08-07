using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using MediatR;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Measure
{
    public class InsertMeasureCommandRequest : IRequest
    {
        public MeasureCommandDto MeasureInsertDto { get; private set; }

        private InsertMeasureCommandRequest() { }

        public static InsertMeasureCommandRequest New(MeasureCommandDto measureCommandDto)
        {
            return new InsertMeasureCommandRequest
            {
                MeasureInsertDto = measureCommandDto
            };
        }
    }
}
