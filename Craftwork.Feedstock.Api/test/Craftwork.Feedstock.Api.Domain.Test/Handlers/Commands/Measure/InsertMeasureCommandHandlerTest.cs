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
    public class InsertMeasureCommandHandlerTest
    {
        private IMeasureRepository _measureRepository;

        private bool inserted;

        public InsertMeasureCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IMeasureRepository>();

            repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Measure>()))
                .Callback(() =>
                {
                    inserted = true;
                });

            //mudar pra setar valor em variável x = true;

            _measureRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_InsertMeasureCommandHandler()
        {
            var insertMeasureCommandHandler = new InsertMeasureCommandHandler(_measureRepository);

            Assert.NotNull(insertMeasureCommandHandler);
        }

        [Fact]
        public async Task Should_Insert_Measure()
        {
            var insertMeasureCommandHandler = new InsertMeasureCommandHandler(_measureRepository);
            var measureCommand = MeasureCommandDto.New("blue", StatusEnum.Enable, Guid.NewGuid());
            var request = InsertMeasureCommandRequest.New(measureCommand);

            await insertMeasureCommandHandler.Handle(request, new CancellationToken());

            Assert.True(inserted);
        }
    }
}
