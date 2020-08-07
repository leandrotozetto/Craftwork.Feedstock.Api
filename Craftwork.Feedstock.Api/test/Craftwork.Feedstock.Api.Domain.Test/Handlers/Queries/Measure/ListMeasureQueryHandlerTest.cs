using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Queries.Measure;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Measure;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Queries.Measure
{
    public class ListMeasureQueryHandlerTest
    {
        private readonly IMeasureRepository _measureRepository;

        public ListMeasureQueryHandlerTest()
        {
            var repository = new Mock<IMeasureRepository>();

            repository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Domain.Entities.Measure, bool>>>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync((Expression<Func<Domain.Entities.Measure, bool>> filter,
               string orderBy, int page, int qtyPerPage) =>
               {
                   var func = filter.Compile();
                   var value = func.Invoke(Domain.Entities.Measure.New("test", StatusEnum.Disable, TenantId.New()));

                   if (value)
                   {
                       return Pagination<Domain.Entities.Measure>.Empty;
                   }
                   else
                   {
                       return Pagination<Domain.Entities.Measure>.New(new Domain.Entities.Measure[]
                       {
                           Domain.Entities.Measure.New("test 1", StatusEnum.Disable, TenantId.New())
                       }, 1, 1, 1);
                   }
               });

            _measureRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_ListMeasureQueryHandler()
        {
            var listMeasureQueryHandler = new ListMeasureQueryHandler(_measureRepository);

            Assert.NotNull(listMeasureQueryHandler);
        }

        [Fact]
        public async Task Should_Get_Measure_Empty_List()
        {
            var listMeasureQueryHandler = new ListMeasureQueryHandler(_measureRepository);
            var request = ListMeasureQueryRequest.New(string.Empty, "ASC", 1, 1);

            var pagination = await listMeasureQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(pagination);
        }

        [Fact]
        public async Task Should_Get_Measure_List()
        {
            var listMeasureQueryHandler = new ListMeasureQueryHandler(_measureRepository);
            var request = ListMeasureQueryRequest.New("nome", "ASC", 1, 1);

            var pagination = await listMeasureQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(pagination);
        }
    }
}
