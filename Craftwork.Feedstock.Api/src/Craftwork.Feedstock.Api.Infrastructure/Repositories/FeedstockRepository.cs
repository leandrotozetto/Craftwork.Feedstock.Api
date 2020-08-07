using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Infrastructure.Repositories
{
    public class FeedstockRepository : IFeedstockRepository
    {
        private readonly IRepository<Domain.Entities.Feedstock> _repository;

        public FeedstockRepository(IRepository<Domain.Entities.Feedstock> repository)
        {
            _repository = repository;
        }

        public async Task<IPagination<Domain.Entities.Feedstock>> ListAsync(Expression<Func<Domain.Entities.Feedstock, bool>> filter = null,
            string orderBy = null, int page = 0, int qtyPerPage = 0)
        {
            return await _repository.ListAsync(filter, orderBy, page, qtyPerPage);
        }

        public async Task<Domain.Entities.Feedstock> GetAsync(FeedstockId feedstockId)
        {
            Ensure.That(feedstockId, nameof(feedstockId)).ArgumentIsNotNull();

            return await _repository.GetAsync(x=> x.FeedstockId.Equals(feedstockId));
        }

        public async Task InsertAsync(Domain.Entities.Feedstock feedstock)
        {
            await _repository.InsertAsync(feedstock);
        }

        public async Task UpdateAsync(Domain.Entities.Feedstock feedstock)
        {
            await _repository.UpdateAsync(feedstock);
        }

        public async Task DeleteAsync(FeedstockId feedstockId)
        {
            var feedstock = await GetAsync(feedstockId);

            _repository.Delete(feedstock);
        }
    }
}
