using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Mappers;
using System;
using System.Linq;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Mappers
{
    public class FeedstockMapperTest
    {
        [Fact]
        public void Should_Convert_To_Dto()
        {
            var feedstock = Domain.Entities.Feedstock.New("test", StatusEnum.Enable, MeasureId.New(), 2, ColorId.New(), TenantId.New());
            var dto = FeedstockMapper.Map(feedstock);

            Assert.NotNull(dto);
        }

        [Fact]
        public void Should_Convert_To_List_Of_Dto()
        {
            var feedstocks = new Domain.Entities.Feedstock[]
                {
                    Domain.Entities.Feedstock.New("test 1", StatusEnum.Enable, MeasureId.New(), 2, ColorId.New(), TenantId.New()),
                    Domain.Entities.Feedstock.New("test 2", StatusEnum.Enable, MeasureId.New(), 2, ColorId.New(), TenantId.New())
                };
            var dtos = FeedstockMapper.Map(feedstocks).ToList();

            Assert.NotEmpty(dtos);
            Assert.NotNull(dtos);
        }

        [Fact]
        public void Should_Convert_To_List_Of_Dto2()
        {
            var feedstocks = new Domain.Entities.Feedstock[] { };
            var dtos = FeedstockMapper.Map(feedstocks).ToList();

            Assert.Empty(dtos);
            Assert.NotNull(dtos);
        }

        [Fact]
        public void Should_Convert_To_Entity()
        {
            var feedstockCommandDto = Domain.Dtos.Feedstock.FeedstockCommandDto.New("teste", StatusEnum.Enable, Guid.NewGuid(), 2, Guid.NewGuid(), Guid.NewGuid());
            var entity = FeedstockMapper.Map(feedstockCommandDto);

            Assert.NotNull(entity);
        }
    }
}
