using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Dtos.Feedstock
{
    public class FeedstockCommandDtoTest
    {
        private readonly string _feedstockName = "km";
        private readonly Status _status = StatusEnum.Enable;
        private readonly Guid _tenantId = Guid.NewGuid();
        private readonly Guid _colorId = Guid.NewGuid();
        private readonly Guid _measureId = Guid.NewGuid();
        private readonly int _stock = 10;

        [Fact]
        public void Should_Create_FeedstockCommandDto()
        {
            var feedstockCommandDtoTest = FeedstockCommandDto.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);

            Assert.NotNull(feedstockCommandDtoTest);
            Assert.Equal(_stock, feedstockCommandDtoTest.Stock);
            Assert.Equal(_tenantId, feedstockCommandDtoTest.TenantId);
            Assert.Equal(_colorId, feedstockCommandDtoTest.ColorId);
            Assert.Equal(_measureId, feedstockCommandDtoTest.MeasureId);
            Assert.Equal(_status, feedstockCommandDtoTest.Status);
            Assert.Equal(_feedstockName, feedstockCommandDtoTest.Name);
        }
    }
}
