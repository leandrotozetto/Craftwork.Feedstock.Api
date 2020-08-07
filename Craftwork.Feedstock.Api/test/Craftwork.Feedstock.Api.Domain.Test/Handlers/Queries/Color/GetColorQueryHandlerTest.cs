using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Queries.Color;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Color;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Queries.Color
{
    public class GetColorQueryHandlerTest
    {
        private readonly IColorRepository _colorRepository;

        public GetColorQueryHandlerTest()
        {
            var repository = new Mock<IColorRepository>();

            repository.Setup(x => x.GetAsync(It.IsAny<ColorId>()))
               .ReturnsAsync((ColorId coloId) =>
               {
                   return Domain.Entities.Color.New("blue", StatusEnum.Enable, TenantId.New());
               });

            _colorRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_GetColorQueryHandler()
        {
            var getColorQueryHandler = new GetColorQueryHandler(_colorRepository);

            Assert.NotNull(getColorQueryHandler);
        }

        [Fact]
        public async Task Should_Get_Color()
        {
            var getColorQueryHandler = new GetColorQueryHandler(_colorRepository);
            var request = GetColorQueryRequest.New(ColorId.New());

            var colorDto = await getColorQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(colorDto);
        }
    }
}