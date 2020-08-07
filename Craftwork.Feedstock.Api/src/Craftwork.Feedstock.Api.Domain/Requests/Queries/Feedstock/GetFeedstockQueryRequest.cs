using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;

namespace Craftwork.Feedstock.Api.Domain.Requests.Queries.Feedstock
{
    public class GetFeedstockQueryRequest : IRequest<FeedstockQueryDto>
    {
        public FeedstockId FeedstockId { get; private set; }

        private GetFeedstockQueryRequest() { }

        public static GetFeedstockQueryRequest New(FeedstockId colorId)
        {
            return new GetFeedstockQueryRequest
            {
                FeedstockId = colorId
            };
        }
    }
}
