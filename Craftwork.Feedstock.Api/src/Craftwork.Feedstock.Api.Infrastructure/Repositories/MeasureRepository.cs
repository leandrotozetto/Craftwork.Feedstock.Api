using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Infrastructure.Repositories
{
    public class MeasureRepository : IMeasureRepository
    {
        private readonly IRepository<Measure> _repository;

        public MeasureRepository(IRepository<Measure> repository)
        {
            _repository = repository;
        }

        public async Task<IPagination<Measure>> ListAsync(Expression<Func<Measure, bool>> filter = null,
            string orderBy = null, int page = 0, int qtyPerPage = 0)
        {
            return await _repository.ListAsync(filter, orderBy, page, qtyPerPage);
        }

        public async Task<Measure> GetAsync(MeasureId measureId)
        {
            Ensure.That(measureId, nameof(measureId)).ArgumentIsNotNull();

            return await _repository.GetAsync(x => x.MeasureId.Equals(measureId));
        }

        public async Task InsertAsync(Measure measure)
        {
            await _repository.InsertAsync(measure);
        }

        public async Task UpdateAsync(Measure measure)
        {
            await _repository.UpdateAsync(measure);
        }

        public async Task DeleteAsync(MeasureId measureId)
        {
            var measure = await GetAsync(measureId);

            _repository.Delete(measure);
        }
    }
}
