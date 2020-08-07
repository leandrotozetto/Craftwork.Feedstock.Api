using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Mappers;
using System;
using System.Linq;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Mappers
{
    public class ColorMapperTest
    {
        [Fact]
        public void Should_Convert_To_Dto()
        {
            var color = Domain.Entities.Color.New("test", StatusEnum.Enable, TenantId.New());
            var dto = ColorMapper.Map(color);

            Assert.NotNull(dto);
        }

        [Fact]
        public void Should_Convert_To_List_Of_Dto()
        {
            var colors = new Domain.Entities.Color[]
                {
                    Domain.Entities.Color.New("test 1", StatusEnum.Enable, TenantId.New()),
                    Domain.Entities.Color.New("test 2", StatusEnum.Enable, TenantId.New())
                };
            var dtos = ColorMapper.Map(colors).ToList();

            Assert.NotEmpty(dtos);
            Assert.NotNull(dtos);
        }

        [Fact]
        public void Should_Convert_To_List_Of_Dto2()
        {
            var colors = new Domain.Entities.Color[] { };
            var dtos = ColorMapper.Map(colors).ToList();

            Assert.Empty(dtos);
            Assert.NotNull(dtos);
        }

        [Fact]
        public void Should_Convert_To_Entity()
        {
            var colorCommandDto = Domain.Dtos.Color.ColorCommandDto.New("teste", StatusEnum.Enable.Value, Guid.NewGuid());
            var entity = ColorMapper.Map(colorCommandDto);

            Assert.NotNull(entity);
        }
    }
}
