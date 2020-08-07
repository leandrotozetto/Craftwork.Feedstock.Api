using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
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
    public class UpdateMeasureCommandHandlerTest
    {
        private IMeasureRepository _measureRepository;

        private bool updated;

        public UpdateMeasureCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IMeasureRepository>();

            repository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Measure>()))
                .Callback(() =>
                {
                    updated = true;
                });

            //mudar pra setar valor em variável x = true;

            _measureRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_UpdateMeasureCommandHandler()
        {
            var updateMeasureCommandHandler = new UpdateMeasureCommandHandler(_measureRepository);

            Assert.NotNull(updateMeasureCommandHandler);
        }

        [Fact]
        public async Task Should_Update_Measure()
        {
            var measureId = Guid.NewGuid();
            var updateMeasureCommandHandler = new UpdateMeasureCommandHandler(_measureRepository);
            var measureCommand = MeasureCommandDto.New("blue", StatusEnum.Enable, Guid.NewGuid());
            var request = UpdateMeasureCommandResquest.New(measureCommand, measureId);

            await updateMeasureCommandHandler.Handle(request, new CancellationToken());

            Assert.True(updated);
            Assert.Equal(measureId, Guid.Parse(request.MeasureId.Value.ToString()));
        }
    }
}
