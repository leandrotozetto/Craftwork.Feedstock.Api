using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Queries.Color;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Color;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Queries.Color
{
    public class ListColorQueryHandlerTest
    {
        private readonly IColorRepository _colorRepository;

        public ListColorQueryHandlerTest()
        {
            var repository = new Mock<IColorRepository>();

            repository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Domain.Entities.Color, bool>>>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync((Expression<Func<Domain.Entities.Color, bool>> filter,
               string orderBy, int page, int qtyPerPage) =>
               {
                   var func = filter.Compile();
                   var value = func.Invoke(Domain.Entities.Color.New("test", StatusEnum.Disable, TenantId.New()));

                   if (value)
                   {
                       return Pagination<Domain.Entities.Color>.Empty;
                   }
                   else
                   {
                       return Pagination<Domain.Entities.Color>.New(new Domain.Entities.Color[]
                       {
                           Domain.Entities.Color.New("test 1", StatusEnum.Disable, TenantId.New())
                       }, 1, 1, 1);
                   }
               });

            _colorRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_ListColorQueryHandler()
        {
            var listColorQueryHandler = new ListColorQueryHandler(_colorRepository);

            Assert.NotNull(listColorQueryHandler);
        }

        [Fact]
        public async Task Should_Get_Color_Empty_List()
        {
            var listColorQueryHandler = new ListColorQueryHandler(_colorRepository);
            var request = ListColorQueryRequest.New(string.Empty, "ASC", 1, 1);

            var pagination = await listColorQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(pagination);
        }

        [Fact]
        public async Task Should_Get_Color_List()
        {
            var listColorQueryHandler = new ListColorQueryHandler(_colorRepository);
            var request = ListColorQueryRequest.New("nome", "ASC", 1, 1);

            var pagination = await listColorQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(pagination);
        }
    }
}
