using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Feedstock
{
    public class DeleteFeedstockCommandHandler : IRequestHandler<DeleteFeedstockCommandRequest>
    {
        private readonly IFeedstockRepository _measureRepository;

        public DeleteFeedstockCommandHandler(IFeedstockRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }

        public async Task<Unit> Handle(DeleteFeedstockCommandRequest request, CancellationToken cancellationToken)
        {
            await _measureRepository.DeleteAsync(request.FeedstockId);

            return Unit.Value;
        }
    }
}
