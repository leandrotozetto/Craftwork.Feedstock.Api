using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
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
    public class InsertFeedstockCommandHandlerTest
    {
        private IFeedstockRepository _feedstockRepository;

        private bool inserted;

        public InsertFeedstockCommandHandlerTest()
        {
            CreateRepository();
        }

        private void CreateRepository()
        {
            var repository = new Mock<IFeedstockRepository>();

            repository.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Feedstock>()))
                .Callback(() =>
                {
                    inserted = true;
                });

            //mudar pra setar valor em variável x = true;

            _feedstockRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_InsertFeedstockCommandHandler()
        {
            var insertFeedstockCommandHandler = new InsertFeedstockCommandHandler(_feedstockRepository);

            Assert.NotNull(insertFeedstockCommandHandler);
        }

        [Fact]
        public async Task Should_Insert_Feedstock()
        {
            var insertFeedstockCommandHandler = new InsertFeedstockCommandHandler(_feedstockRepository);
            var feedstockCommand = FeedstockCommandDto.New("blue", StatusEnum.Enable, Guid.NewGuid(), 3, Guid.NewGuid(), Guid.NewGuid());
            var request = InsertFeedstockCommandRequest.New(feedstockCommand);

            await insertFeedstockCommandHandler.Handle(request, new CancellationToken());

            Assert.True(inserted);
        }
    }
}
