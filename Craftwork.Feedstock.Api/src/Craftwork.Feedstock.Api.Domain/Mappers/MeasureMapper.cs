using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System.Collections.Generic;

namespace Craftwork.Feedstock.Api.Domain.Mappers
{
    public static class MeasureMapper
    {
        public static Measure Map(MeasureCommandDto measureInsertDto)
        {
            return Measure.New(measureInsertDto.Name, measureInsertDto.Status, TenantId.New(measureInsertDto.TenantId));
        }

        public static MeasureQueryDto Map(Measure measure)
        {
            return MeasureQueryDto.New(measure.Name, measure.Status);
        }

        public static IEnumerable<MeasureQueryDto> Map(IEnumerable<Entities.Measure> measures)
        {
            foreach (var measure in measures)
            {
                yield return Map(measure);
            }
        }
    }
}
