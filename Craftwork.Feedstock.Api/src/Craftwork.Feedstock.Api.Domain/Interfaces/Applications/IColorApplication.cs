using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using System;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Interfaces.Applications
{
    public interface IColorApplication
    {
        Task<IResponse> DeleteAsync(Guid id);

        Task<ColorQueryDto> GetAsync(Guid colorId);
    }
}
