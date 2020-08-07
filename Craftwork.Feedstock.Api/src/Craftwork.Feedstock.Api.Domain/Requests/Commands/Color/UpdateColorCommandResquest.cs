using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using MediatR;
using System;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Color
{
    public class UpdateColorCommandResquest : IRequest
    {
        public ColorCommandDto ColorCommandDto { get; private set; }

        public ColorId ColorId { get; private set; }

        private UpdateColorCommandResquest() { }

        public static UpdateColorCommandResquest New(ColorCommandDto colorCommandDto, Guid colorId)
        {
            return new UpdateColorCommandResquest
            {
                ColorCommandDto = colorCommandDto,
                ColorId = ColorId.New(colorId)
            };
        }
    }
}
