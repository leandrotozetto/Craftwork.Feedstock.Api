using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Interfaces.Repositories
{
    public interface IFeedstockRepository
    {
        Task<Entities.Feedstock> GetAsync(FeedstockId feedstockId);

        Task InsertAsync(Entities.Feedstock feedstock);

        Task UpdateAsync(Entities.Feedstock feedstock);

        Task<IPagination<Entities.Feedstock>> ListAsync(Expression<Func<Entities.Feedstock, bool>> filter = null,
            string orderBy = null, int page = 0, int qtyPerPage = 0);

        Task DeleteAsync(FeedstockId feedstockId);
    }
}
