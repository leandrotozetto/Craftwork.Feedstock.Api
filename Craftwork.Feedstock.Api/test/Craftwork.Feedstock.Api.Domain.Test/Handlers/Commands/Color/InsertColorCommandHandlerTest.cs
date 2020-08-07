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
    public class InsertColorCommandHandlerTest
    {
        private IColorRepository _colorRepository;

        private bool inserted;

        public InsertColorCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IColorRepository>();

            repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Color>()))
                .Callback(() =>
                {
                    inserted = true;
                });

            //mudar pra setar valor em variável x = true;

            _colorRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_InsertColorCommandHandler()
        {
            var insertColorCommandHandler = new InsertColorCommandHandler(_colorRepository);

            Assert.NotNull(insertColorCommandHandler);
        }

        [Fact]
        public async Task Should_Insert_Color()
        {
            var insertColorCommandHandler = new InsertColorCommandHandler(_colorRepository);
            var colorCommand = ColorCommandDto.New("blue", StatusEnum.Enable.Value, Guid.NewGuid());
            var request = InsertColorCommandRequest.New(colorCommand);

            await insertColorCommandHandler.Handle(request, new CancellationToken());

            Assert.True(inserted);
        }
    }
}
