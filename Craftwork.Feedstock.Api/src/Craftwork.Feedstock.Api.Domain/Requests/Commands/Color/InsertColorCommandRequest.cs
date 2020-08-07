using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using MediatR;

namespace Craftwork.Feedstock.Api.Domain.Requests.Commands.Color
{
    public class InsertColorCommandRequest : IRequest
    {
        public ColorCommandDto ColorInsertDto { get; private set; }

        private InsertColorCommandRequest() { }

        public static InsertColorCommandRequest New(ColorCommandDto colorCommandDto)
        {
            return new InsertColorCommandRequest
            {
                ColorInsertDto = colorCommandDto
            };
        }
    }
}
