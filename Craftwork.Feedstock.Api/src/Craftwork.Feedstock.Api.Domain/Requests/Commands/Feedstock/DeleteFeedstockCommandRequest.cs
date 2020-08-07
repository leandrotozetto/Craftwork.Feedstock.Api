using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;
using System;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock
{
    public class DeleteFeedstockCommandRequest : IRequest
    {
        public FeedstockId FeedstockId { get; private set; }

        private DeleteFeedstockCommandRequest() { }

        public static DeleteFeedstockCommandRequest New(Guid feedstockId)
        {
            return new DeleteFeedstockCommandRequest
            {
                FeedstockId = FeedstockId.New(feedstockId)
            };
        }
    }
}
