using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Queries.Feedstock;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Feedstock;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Queries.Feedstock
{
    public class GetFeedstockQueryHandlerTest
    {
        private readonly IFeedstockRepository _feedstockRepository;

        public GetFeedstockQueryHandlerTest()
        {
            var repository = new Mock<IFeedstockRepository>();

            repository.Setup(x => x.GetAsync(It.IsAny<FeedstockId>()))
               .ReturnsAsync((FeedstockId coloId) =>
               {
                   //TODO: change to full entity
                   return Domain.Entities.Feedstock.New("blue", StatusEnum.Enable, MeasureId.New(), 2,  ColorId.New(), TenantId.New());
               });

            _feedstockRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_GetFeedstockQueryHandler()
        {
            var getFeedstockQueryHandler = new GetFeedstockQueryHandler(_feedstockRepository);

            Assert.NotNull(getFeedstockQueryHandler);
        }

        [Fact]
        public async Task Should_Get_Feedstock()
        {
            var getFeedstockQueryHandler = new GetFeedstockQueryHandler(_feedstockRepository);
            var request = GetFeedstockQueryRequest.New(FeedstockId.New());

            var feedstockDto = await getFeedstockQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(feedstockDto);
        }
    }
}