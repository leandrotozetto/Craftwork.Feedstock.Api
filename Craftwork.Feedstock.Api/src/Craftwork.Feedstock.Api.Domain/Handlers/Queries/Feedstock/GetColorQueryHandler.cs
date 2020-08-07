using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Feedstock;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Queries.Feedstock
{
    public class GetFeedstockQueryHandler : IRequestHandler<GetFeedstockQueryRequest, FeedstockQueryDto>
    {
        private readonly IFeedstockRepository _feedstockRepository;

        public GetFeedstockQueryHandler(IFeedstockRepository feedstockRepository)
        {
            _feedstockRepository = feedstockRepository;
        }

        public async Task<FeedstockQueryDto> Handle(GetFeedstockQueryRequest request, CancellationToken cancellationToken)
        {
            var feedstock = await _feedstockRepository.GetAsync(request.FeedstockId);
            var feedstockDto = FeedstockMapper.Map(feedstock);

            return feedstockDto;
        }
    }
}


//public class UsersController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public UsersController(IMediator mediator)
//    {
//        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
//    }

//    [HttpGet("{id}")]
//    public async Task<Order> Get(int id)
//    {
//        var user = await _mediator.Send(new GetUserDetailQuery(id));
//        if user == null {
//            return NotFound();
//        }
//        return Ok(user);
//    }
//}