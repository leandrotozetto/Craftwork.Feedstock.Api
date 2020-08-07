using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;
using System;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Color
{
    public class DeleteColorCommandRequest : IRequest<bool>
    {
        public ColorId ColorId { get; private set; }

        private DeleteColorCommandRequest() { }

        public static DeleteColorCommandRequest New(Guid colorId)
        {
            return new DeleteColorCommandRequest
            {
                ColorId = ColorId.New(colorId)
            };
        }
    }
}
