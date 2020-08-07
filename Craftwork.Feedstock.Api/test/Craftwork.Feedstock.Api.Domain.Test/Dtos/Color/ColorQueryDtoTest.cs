using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Dtos.Color
{
    public class ColorQueryDtoTest
    {
        private readonly string _colorName = "blue";
        private readonly bool _status = StatusEnum.Enable.Value;

        [Fact]
        public void Should_Create_ColorQueryDto()
        {
            var colorCommandDtoTest = ColorQueryDto.New(_colorName, _status);

            Assert.NotNull(colorCommandDtoTest);
            Assert.Equal(_status, colorCommandDtoTest.Status);
            Assert.Equal(_colorName, colorCommandDtoTest.Name);
        }
    }
}
