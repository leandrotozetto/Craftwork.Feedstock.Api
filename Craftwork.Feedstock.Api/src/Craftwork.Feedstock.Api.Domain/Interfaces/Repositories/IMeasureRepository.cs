using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Interfaces.Repositories
{
    public interface IMeasureRepository
    {
        Task<Measure> GetAsync(MeasureId measureId);

        Task InsertAsync(Measure measure);

        Task UpdateAsync(Measure measure);

        Task<IPagination<Measure>> ListAsync(Expression<Func<Measure, bool>> filter = null,
            string orderBy = null, int page = 0, int qtyPerPage = 0);

        Task DeleteAsync(MeasureId measureId);
    }
}
