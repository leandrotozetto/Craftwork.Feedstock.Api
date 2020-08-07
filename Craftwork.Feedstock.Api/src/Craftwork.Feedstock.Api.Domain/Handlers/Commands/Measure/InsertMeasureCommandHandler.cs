using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Measure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Measure
{
    public class InsertMeasureCommandHandler : IRequestHandler<InsertMeasureCommandRequest>
    {
        private readonly IMeasureRepository _measureRepository;

        public InsertMeasureCommandHandler(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }

        public async Task<Unit> Handle(InsertMeasureCommandRequest request, CancellationToken cancellationToken)
        {
            var measure = MeasureMapper.Map(request.MeasureInsertDto);

            await _measureRepository.InsertAsync(measure);

            return Unit.Value;
        }
    }
}
