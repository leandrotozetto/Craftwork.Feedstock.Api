using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System.Collections.Generic;

namespace Craftwork.Feedstock.Api.Domain.Mappers
{
    public static class ColorMapper
    {
        public static Color Map(ColorCommandDto colorCommandDto)
        {
            return Color.New(colorCommandDto.Name, StatusEnum.Convert(colorCommandDto.Status), TenantId.New(colorCommandDto.ColorId));
        }

        public static ColorQueryDto Map(Color color)
        {
            if(color is null)
            {
                return ColorQueryDto.Empty;
            }

            return ColorQueryDto.New(color.Name, color.Status.Value);
        }

        public static IEnumerable<ColorQueryDto> Map(IEnumerable<Color> colors)
        {
            foreach (var color in colors)
            {
                yield return Map(color);
            }
        }
    }
}
