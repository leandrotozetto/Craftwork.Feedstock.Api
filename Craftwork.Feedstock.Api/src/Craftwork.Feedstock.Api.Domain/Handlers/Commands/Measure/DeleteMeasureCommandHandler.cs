using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Measure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Measure
{
    public class DeleteMeasureCommandHandler : IRequestHandler<DeleteMeasureCommandRequest>
    {
        private readonly IMeasureRepository _measureRepository;

        public DeleteMeasureCommandHandler(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }

        public async Task<Unit> Handle(DeleteMeasureCommandRequest request, CancellationToken cancellationToken)
        {
            await _measureRepository.DeleteAsync(request.MeasureId);

            return Unit.Value;
        }
    }
}
