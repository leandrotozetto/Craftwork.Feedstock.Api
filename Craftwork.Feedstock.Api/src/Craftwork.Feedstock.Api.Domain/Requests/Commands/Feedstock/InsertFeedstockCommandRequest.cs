using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using MediatR;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock
{
    public class InsertFeedstockCommandRequest : IRequest
    {
        public FeedstockCommandDto FeedstockCommandDto { get; private set; }

        private InsertFeedstockCommandRequest() { }

        public static InsertFeedstockCommandRequest New(FeedstockCommandDto feedstockCommandDto)
        {
            return new InsertFeedstockCommandRequest
            {
                FeedstockCommandDto = feedstockCommandDto
            };
        }
    }
}
