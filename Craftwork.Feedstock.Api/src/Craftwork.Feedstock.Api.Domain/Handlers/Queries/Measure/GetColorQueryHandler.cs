using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Measure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Queries.Measure
{
    public class GetMeasureQueryHandler : IRequestHandler<GetMeasureQueryRequest, MeasureQueryDto>
    {
        private readonly IMeasureRepository _measureRepository;

        public GetMeasureQueryHandler(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }

        public async Task<MeasureQueryDto> Handle(GetMeasureQueryRequest request, CancellationToken cancellationToken)
        {
            var measure = await _measureRepository.GetAsync(request.MeasureId);
            var measureDto = MeasureMapper.Map(measure);

            return measureDto;
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