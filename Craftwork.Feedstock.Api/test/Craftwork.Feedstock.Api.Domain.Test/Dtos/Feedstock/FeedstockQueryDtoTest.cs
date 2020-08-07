using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Dtos.Feedstock
{
    public class FeedstockQueryDtoTest
    {
        private readonly string _feedstockName = "t-shirt";
        private readonly string _colorName = "blue";
        private readonly string _measureName = "km";
        private readonly Status _status = StatusEnum.Enable;

        [Fact]
        public void Should_Create_FeedstockQueryDto()
        {
            var feedstockCommandDtoTest = FeedstockQueryDto.New(_feedstockName, _status, _colorName, _measureName);

            Assert.NotNull(feedstockCommandDtoTest);
            Assert.Equal(_status, feedstockCommandDtoTest.Status);
            Assert.Equal(_feedstockName, feedstockCommandDtoTest.Name);
            Assert.Equal(_colorName, feedstockCommandDtoTest.ColorName);
            Assert.Equal(_measureName, feedstockCommandDtoTest.MeasureName);
        }
    }
}
