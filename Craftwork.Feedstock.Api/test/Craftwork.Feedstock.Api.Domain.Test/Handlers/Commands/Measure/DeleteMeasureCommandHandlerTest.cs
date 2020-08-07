using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Commands.Measure;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Measure;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Commands.Measure
{
    public class DeleteMeasureCommandHandlerTest
    {
        private IMeasureRepository _measureRepository;

        private bool deleted;

        public DeleteMeasureCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IMeasureRepository>();

            repository.Setup(x => x.DeleteAsync(It.IsAny<MeasureId>()))
                .Callback(() =>
                {
                    deleted = true;
                });

            //mudar pra setar valor em variável x = true;

            _measureRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_DeleteMeasureCommandHandler()
        {
            var deleteMeasureCommandHandler = new DeleteMeasureCommandHandler(_measureRepository);

            Assert.NotNull(deleteMeasureCommandHandler);
        }

        [Fact]
        public async Task Should_Delete_Measure()
        {
            var deleteMeasureCommandHandler = new DeleteMeasureCommandHandler(_measureRepository);
            var request = DeleteMeasureCommandRequest.New(Guid.NewGuid());

            await deleteMeasureCommandHandler.Handle(request, new CancellationToken());

            Assert.True(deleted);
        }
    }
}
