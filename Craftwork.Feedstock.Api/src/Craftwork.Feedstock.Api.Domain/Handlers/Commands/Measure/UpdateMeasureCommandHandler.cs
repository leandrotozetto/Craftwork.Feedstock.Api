using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Measure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Measure
{
    public class UpdateMeasureCommandHandler : IRequestHandler<UpdateMeasureCommandResquest>
    {
        private readonly IMeasureRepository _measureRepository;

        public UpdateMeasureCommandHandler(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }

        public async Task<Unit> Handle(UpdateMeasureCommandResquest request, CancellationToken cancellationToken)
        {
            var measure = MeasureMapper.Map(request.MeasureCommandDto);

            await _measureRepository.UpdateAsync(measure);

            return Unit.Value;
        }
    }
}
