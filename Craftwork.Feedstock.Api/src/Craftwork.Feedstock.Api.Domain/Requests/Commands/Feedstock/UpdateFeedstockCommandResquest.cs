using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;
using System;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock
{
    public class UpdateFeedstockCommandResquest : IRequest
    {
        public FeedstockCommandDto FeedstockCommandDto { get; private set; }

        public FeedstockId FeedstockId { get; private set; }

        private UpdateFeedstockCommandResquest() { }

        public static UpdateFeedstockCommandResquest New(FeedstockCommandDto feedstockCommandDto, Guid feedstockId)
        {
            return new UpdateFeedstockCommandResquest
            {
                FeedstockCommandDto = feedstockCommandDto,
                FeedstockId = FeedstockId.New(feedstockId)
            };
        }
    }
}
