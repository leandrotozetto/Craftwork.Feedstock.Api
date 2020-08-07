using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Handlers.Commands.Color;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Color;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Commands.Color
{
    public class UpdateColorCommandHandlerTest
    {
        private IColorRepository _colorRepository;

        private bool updated;

        public UpdateColorCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IColorRepository>();

            repository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Color>()))
                .Callback(() =>
                {
                    updated = true;
                });

            //mudar pra setar valor em variável x = true;

            _colorRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_UpdateColorCommandHandler()
        {
            var updateColorCommandHandler = new UpdateColorCommandHandler(_colorRepository);

            Assert.NotNull(updateColorCommandHandler);
        }

        [Fact]
        public async Task Should_Update_Color()
        {
            var colorId = Guid.NewGuid();
            var updateColorCommandHandler = new UpdateColorCommandHandler(_colorRepository);
            var colorCommand = ColorCommandDto.New("blue", StatusEnum.Enable.Value, Guid.NewGuid());
            var request = UpdateColorCommandResquest.New(colorCommand, colorId);

            await updateColorCommandHandler.Handle(request, new CancellationToken());
            
            Assert.True(updated);
            Assert.Equal(colorId, Guid.Parse(request.ColorId.Value.ToString()));
        }
    }
}
