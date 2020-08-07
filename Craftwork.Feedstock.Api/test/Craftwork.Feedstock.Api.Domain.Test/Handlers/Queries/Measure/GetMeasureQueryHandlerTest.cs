using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Handlers.Queries.Measure;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Measure;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Handlers.Queries.Measure
{
    public class GetMeasureQueryHandlerTest
    {
        private readonly IMeasureRepository _measureRepository;

        public GetMeasureQueryHandlerTest()
        {
            var repository = new Mock<IMeasureRepository>();

            repository.Setup(x => x.GetAsync(It.IsAny<MeasureId>()))
               .ReturnsAsync((MeasureId coloId) =>
               {
                   //TODO: change to full entity
                   return Domain.Entities.Measure.New("blue", StatusEnum.Enable, TenantId.New());
               });

            _measureRepository = repository.Object;
        }

        [Fact]
        public void Should_Create_GetMeasureQueryHandler()
        {
            var getMeasureQueryHandler = new GetMeasureQueryHandler(_measureRepository);

            Assert.NotNull(getMeasureQueryHandler);
        }

        [Fact]
        public async Task Should_Get_Measure()
        {
            var getMeasureQueryHandler = new GetMeasureQueryHandler(_measureRepository);
            var request = GetMeasureQueryRequest.New(MeasureId.New());

            var measureDto = await getMeasureQueryHandler.Handle(request, new CancellationToken());

            Assert.NotNull(measureDto);
        }
    }
}