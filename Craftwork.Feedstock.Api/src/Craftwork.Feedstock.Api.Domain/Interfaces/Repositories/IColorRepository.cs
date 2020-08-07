using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Interfaces.Repositories
{
    public interface IColorRepository //: IDisposable
    {
        Task<Color> GetAsync(ColorId colorId);

        Task InsertAsync(Color color);

        Task UpdateAsync(Color color);

        Task<IPagination<Color>> ListAsync(Expression<Func<Color, bool>> filter = null,
            string orderBy = null, int page = 0, int qtyPerPage = 0);

        Task DeleteAsync(ColorId colorId);
    }
}
