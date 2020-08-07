using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System.Collections.Generic;

namespace Craftwork.Feedstock.Api.Domain.Mappers
{
    public static class FeedstockMapper
    {
        public static Entities.Feedstock Map(FeedstockCommandDto feedstockInsertDto)
        {
            return Entities.Feedstock.New(feedstockInsertDto.Name, feedstockInsertDto.Status, 
                MeasureId.New(feedstockInsertDto.MeasureId), feedstockInsertDto.Stock, 
                ColorId.New(feedstockInsertDto.ColorId), TenantId.New(feedstockInsertDto.TenantId));
        }

        public static FeedstockQueryDto Map(Entities.Feedstock feedstock)
        {
            return FeedstockQueryDto.New(feedstock.Name, StatusEnum.Disable,  string.Empty, string.Empty);
        }

        public static IEnumerable<FeedstockQueryDto> Map(IEnumerable<Entities.Feedstock> feedstocks)
        {
            foreach (var feedstock in feedstocks)
            {
                yield return Map(feedstock);
            }
        }
    }
}
