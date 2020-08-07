using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Mappers;
using System;
using System.Linq;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Mappers
{
    public class MeasureMapperTest
    {
        [Fact]
        public void Should_Convert_To_Dto()
        {
            var measure = Domain.Entities.Measure.New("test", StatusEnum.Enable, TenantId.New());
            var dto = MeasureMapper.Map(measure);

            Assert.NotNull(dto);
        }

        [Fact]
        public void Should_Convert_To_List_Of_Dto()
        {
            var measures = new Domain.Entities.Measure[]
                {
                    Domain.Entities.Measure.New("test 1", StatusEnum.Enable, TenantId.New()),
                    Domain.Entities.Measure.New("test 2", StatusEnum.Enable, TenantId.New())
                };
            var dtos = MeasureMapper.Map(measures).ToList();

            Assert.NotEmpty(dtos);
            Assert.NotNull(dtos);
        }

        [Fact]
        public void Should_Convert_To_List_Of_Dto2()
        {
            var measures = new Domain.Entities.Measure[] { };
            var dtos = MeasureMapper.Map(measures).ToList();

            Assert.Empty(dtos);
            Assert.NotNull(dtos);
        }

        [Fact]
        public void Should_Convert_To_Entity()
        {
            var measureCommandDto = Domain.Dtos.Measure.MeasureCommandDto.New("teste", StatusEnum.Enable, Guid.NewGuid());
            var entity = MeasureMapper.Map(measureCommandDto);

            Assert.NotNull(entity);
        }
    }
}
