using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Measure
{
    public class UpdateFeedstockCommandHandler : IRequestHandler<UpdateFeedstockCommandResquest>
    {
        private readonly IFeedstockRepository _feedstockRepository;

        public UpdateFeedstockCommandHandler(IFeedstockRepository feedstockRepository)
        {
            _feedstockRepository = feedstockRepository;
        }

        public async Task<Unit> Handle(UpdateFeedstockCommandResquest request, CancellationToken cancellationToken)
        {
            var measure = FeedstockMapper.Map(request.FeedstockCommandDto);

            await _feedstockRepository.UpdateAsync(measure);

            return Unit.Value;
        }
    }
}
