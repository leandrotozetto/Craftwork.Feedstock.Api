using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Dtos.Measure
{
    public class MeasureCommandDtoTest
    {
        private readonly string _measureName = "km";
        private readonly Status _status = StatusEnum.Enable;
        private readonly Guid _tenantId = Guid.NewGuid();

        [Fact]
        public void Should_Create_MeasureCommandDto()
        {
            var measureCommandDtoTest = MeasureCommandDto.New(_measureName, _status, _tenantId);

            Assert.NotNull(measureCommandDtoTest);
            Assert.Equal(_tenantId, measureCommandDtoTest.TenantId);
            Assert.Equal(_status, measureCommandDtoTest.Status);
            Assert.Equal(_measureName, measureCommandDtoTest.Name);
        }
    }
}
