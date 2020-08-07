using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Color;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Color
{
    public class InsertColorCommandHandler : IRequestHandler<InsertColorCommandRequest>
    {
        private readonly IColorRepository _colorRepository;

        public InsertColorCommandHandler(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<Unit> Handle(InsertColorCommandRequest request, CancellationToken cancellationToken)
        {
            var color = ColorMapper.Map(request.ColorInsertDto);

            await _colorRepository.InsertAsync(color);

            return Unit.Value;
        }
    }
}
