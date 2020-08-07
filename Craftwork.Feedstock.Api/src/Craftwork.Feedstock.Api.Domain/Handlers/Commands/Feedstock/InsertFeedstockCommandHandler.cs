using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Feedstock
{
    public class InsertFeedstockCommandHandler : IRequestHandler<InsertFeedstockCommandRequest>
    {
        private readonly IFeedstockRepository _feedstockRepository;

        public InsertFeedstockCommandHandler(IFeedstockRepository feedstockRepository)
        {
            _feedstockRepository = feedstockRepository;
        }

        public async Task<Unit> Handle(InsertFeedstockCommandRequest request, CancellationToken cancellationToken)
        {
            var feedstock = FeedstockMapper.Map(request.FeedstockCommandDto);

            await _feedstockRepository.InsertAsync(feedstock);

            return Unit.Value;
        }
    }
}
