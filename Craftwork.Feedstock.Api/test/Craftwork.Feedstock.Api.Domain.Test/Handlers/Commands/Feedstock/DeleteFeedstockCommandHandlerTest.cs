using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Commands.Feedstock;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Feedstock;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Commands.Feedstock
{
    public class DeleteFeedstockCommandHandlerTest
    {
        private IFeedstockRepository _feedstockRepository;

        private bool deleted;

        public DeleteFeedstockCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IFeedstockRepository>();

            repository.Setup(x => x.DeleteAsync(It.IsAny<FeedstockId>()))
                .Callback(() =>
                {
                    deleted = true;
                });

            //mudar pra setar valor em variável x = true;

            _feedstockRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_DeleteFeedstockCommandHandler()
        {
            var deleteFeedstockCommandHandler = new DeleteFeedstockCommandHandler(_feedstockRepository);

            Assert.NotNull(deleteFeedstockCommandHandler);
        }

        [Fact]
        public async Task Should_Delete_Feedstock()
        {
            var deleteFeedstockCommandHandler = new DeleteFeedstockCommandHandler(_feedstockRepository);
            var request = DeleteFeedstockCommandRequest.New(Guid.NewGuid());

            await deleteFeedstockCommandHandler.Handle(request, new CancellationToken());

            Assert.True(deleted);
        }
    }
}
