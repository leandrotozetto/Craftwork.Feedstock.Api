using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Queries.Feedstock;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Feedstock;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Queries.Feedstock
{
    public class ListFeedstockQueryHandlerTest
    {
        private readonly IFeedstockRepository _feedstockRepository;

        public ListFeedstockQueryHandlerTest()
        {
            var repository = new Mock<IFeedstockRepository>();

            repository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Domain.Entities.Feedstock, bool>>>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync((Expression<Func<Domain.Entities.Feedstock, bool>> filter,
               string orderBy, int page, int qtyPerPage) =>
               {
                   var func = filter.Compile();
                   var value = func.Invoke(Domain.Entities.Feedstock.New("test", StatusEnum.Disable, MeasureId.New(), 1, ColorId.New(), TenantId.New()));

                   if (value)
                   {
                       return Pagination<Domain.Entities.Feedstock>.Empty;
                   }
                   else
                   {
                       return Pagination<Domain.Entities.Feedstock>.New(new Domain.Entities.Feedstock[]
                       {
                           Domain.Entities.Feedstock.New("test 1", StatusEnum.Disable, MeasureId.New(), 1, ColorId.New(), TenantId.New())
                       }, 1, 1, 1);
                   }
               });

            _feedstockRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_ListFeedstockQueryHandler()
        {
            var listFeedstockQueryHandler = new ListFeedstockQueryHandler(_feedstockRepository);

            Assert.NotNull(listFeedstockQueryHandler);
        }

        [Fact]
        public async Task Should_Get_Feedstock_Empty_List()
        {
            var listMeasureQueryHandler = new ListFeedstockQueryHandler(_feedstockRepository);
            var request = ListFeedstockQueryRequest.New(string.Empty, "ASC", 1, 1);

            var pagination = await listMeasureQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(pagination);
        }

        [Fact]
        public async Task Should_Get_Feedstock_List()
        {
            var listFeedstockQueryHandler = new ListFeedstockQueryHandler(_feedstockRepository);
            var request = ListFeedstockQueryRequest.New("nome", "ASC", 1, 1);

            var pagination = await listFeedstockQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(pagination);
        }
    }
}
