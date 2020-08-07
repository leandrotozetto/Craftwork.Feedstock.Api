using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Color;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Color
{
    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommandResquest>
    {
        private readonly IColorRepository _colorRepository;

        public UpdateColorCommandHandler(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<Unit> Handle(UpdateColorCommandResquest request, CancellationToken cancellationToken)
        {
            var color = ColorMapper.Map(request.ColorCommandDto);

            await _colorRepository.UpdateAsync(color);

            return Unit.Value;
        }
    }
}
