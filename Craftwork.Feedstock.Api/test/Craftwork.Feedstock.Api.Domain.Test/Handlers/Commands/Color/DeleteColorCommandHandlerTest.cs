using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
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
    public class DeleteColorCommandHandlerTest
    {
        private IColorRepository _colorRepository;

        private bool deleted;

        public DeleteColorCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IColorRepository>();

            repository.Setup(x => x.DeleteAsync(It.IsAny<ColorId>()))
                .Callback(() =>
                {
                    deleted = true;
                });

            _colorRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_DeleteColorCommandHandler()
        {
            var deleteColorCommandHandler = new DeleteColorCommandHandler(_colorRepository);

            Assert.NotNull(deleteColorCommandHandler);
        }

        [Fact]
        public async Task Should_Delete_Color()
        {
            var deleteColorCommandHandler = new DeleteColorCommandHandler(_colorRepository);
            var request = DeleteColorCommandRequest.New(Guid.NewGuid());

            await deleteColorCommandHandler.Handle(request, new CancellationToken());

            Assert.True(deleted);
        }
    }
}
