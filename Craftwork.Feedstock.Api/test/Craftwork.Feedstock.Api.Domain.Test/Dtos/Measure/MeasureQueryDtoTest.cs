using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Dtos.Measure
{
    public class MeasureQueryDtoTest
    {
        private readonly string _measureName = "km";
        private readonly Status _status = StatusEnum.Enable;

        [Fact]
        public void Should_Create_MeasureQueryDto()
        {
            var measureCommandDtoTest = MeasureQueryDto.New(_measureName, _status);

            Assert.NotNull(measureCommandDtoTest);
            Assert.Equal(_status, measureCommandDtoTest.Status);
            Assert.Equal(_measureName, measureCommandDtoTest.Name);
        }
    }
}
