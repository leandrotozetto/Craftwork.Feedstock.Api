using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;
using System;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Measure
{
    public class UpdateMeasureCommandResquest : IRequest
    {
        public MeasureCommandDto MeasureCommandDto { get; private set; }

        public MeasureId MeasureId { get; private set; }

        private UpdateMeasureCommandResquest() { }

        public static UpdateMeasureCommandResquest New(MeasureCommandDto measureCommandDto, Guid measureId)
        {
            return new UpdateMeasureCommandResquest
            {
                MeasureCommandDto = measureCommandDto,
                MeasureId = MeasureId.New(measureId)
            };
        }
    }
}
