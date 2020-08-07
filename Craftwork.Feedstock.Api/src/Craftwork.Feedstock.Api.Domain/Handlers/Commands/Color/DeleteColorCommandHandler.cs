using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Color;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Commands.Color
{
    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommandRequest, bool>
    {
        private readonly IColorRepository _colorRepository;

        public DeleteColorCommandHandler(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<bool> Handle(DeleteColorCommandRequest request, CancellationToken cancellationToken)
        {
            await _colorRepository.DeleteAsync(request.ColorId);

            return  true;
        }
    }
}
