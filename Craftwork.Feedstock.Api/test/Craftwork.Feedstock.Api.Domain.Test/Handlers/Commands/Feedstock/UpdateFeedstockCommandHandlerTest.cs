using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using Craftwork.Feedstock.Api.Domain.Handlers.Commands.Measure;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Commands.Feedstock
{
    public class UpdateFeedstockCommandHandlerTest
    {
        private IFeedstockRepository _feedstockRepository;

        private bool updated;

        public UpdateFeedstockCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IFeedstockRepository>();

            repository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Feedstock>()))
                .Callback(() =>
                {
                    updated = true;
                });

            //mudar pra setar valor em variável x = true;

            _feedstockRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_UpdateFeedstockCommandHandler()
        {
            var updateFeedstockCommandHandler = new UpdateFeedstockCommandHandler(_feedstockRepository);

            Assert.NotNull(updateFeedstockCommandHandler);
        }

        [Fact]
        public async Task Should_Update_Feedstock()
        {
            var feedstockId = Guid.NewGuid();
            var updateFeedstockCommandHandler = new UpdateFeedstockCommandHandler(_feedstockRepository);
            var feedstockCommand = FeedstockCommandDto.New("blue", StatusEnum.Enable, Guid.NewGuid(), 2, Guid.NewGuid(), Guid.NewGuid());
            var request = UpdateFeedstockCommandResquest.New(feedstockCommand, feedstockId);

            await updateFeedstockCommandHandler.Handle(request, new CancellationToken());

            Assert.True(updated);
            Assert.Equal(feedstockId, Guid.Parse(request.FeedstockId.Value.ToString()));
        }
    }
}
