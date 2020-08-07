using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;
using System;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Measure
{
    public class DeleteMeasureCommandRequest : IRequest
    {
        public MeasureId MeasureId { get; private set; }

        private DeleteMeasureCommandRequest() { }

        public static DeleteMeasureCommandRequest New(Guid measureId)
        {
            return new DeleteMeasureCommandRequest
            {
                MeasureId = MeasureId.New(measureId)
            };
        }
    }
}
