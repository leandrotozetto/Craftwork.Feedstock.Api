using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Color;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Queries.Color
{
    public class GetColorQueryHandler : IRequestHandler<GetColorQueryRequest, ColorQueryDto>
    {
        private readonly IColorRepository _colorRepository;

        public GetColorQueryHandler(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<ColorQueryDto> Handle(GetColorQueryRequest request, CancellationToken cancellationToken)
        {
            var color = await _colorRepository.GetAsync(request.ColorId);
            var colorDto = ColorMapper.Map(color);

            return colorDto;
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