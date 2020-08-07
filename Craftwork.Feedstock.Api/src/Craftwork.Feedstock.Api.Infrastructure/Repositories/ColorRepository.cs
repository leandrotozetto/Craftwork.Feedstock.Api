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
    public class ColorRepository : IColorRepository
    {
        private readonly IRepository<Color> _repository;

        public ColorRepository(IRepository<Color> repository)
        {
            _repository = repository;
        }

        public async Task<IPagination<Color>> ListAsync(Expression<Func<Color, bool>> filter = null,
            string orderBy = null, int page = 0, int qtyPerPage = 0)
        {
            return await _repository.ListAsync(filter, orderBy, page, qtyPerPage);
        }

        public async Task<Color> GetAsync(ColorId colorId)
        {
            Ensure.That(colorId, nameof(colorId)).ArgumentIsNotNull();

            return await _repository.GetAsync(x => x.ColorId.Equals(colorId));
        }

        public async Task InsertAsync(Color color)
        {
            await _repository.InsertAsync(color);
        }

        public async Task UpdateAsync(Color color)
        {
            await _repository.UpdateAsync(color);
        }

        public async Task DeleteAsync(ColorId colorId)
        {
            var color = await GetAsync(colorId);

            _repository.Delete(color);
        }
    }
}
