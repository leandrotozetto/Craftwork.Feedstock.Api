using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Dtos.Color
{
    public class ColorCommandDtoTest
    {
        private readonly string _colorName = "blue";
        private readonly Status _status = StatusEnum.Enable;
        private readonly Guid _tenantId = Guid.NewGuid();

        [Fact]
        public void Should_Create_ColorCommandDto()
        {
            var colorCommandDtoTest = ColorCommandDto.New(_colorName, _status.Value, _tenantId);

            Assert.NotNull(colorCommandDtoTest);
            Assert.Equal(_tenantId, colorCommandDtoTest.ColorId);
            Assert.Equal(_status, StatusEnum.Convert(colorCommandDtoTest.Status));
            Assert.Equal(_colorName, colorCommandDtoTest.Name);
        }
    }
}
