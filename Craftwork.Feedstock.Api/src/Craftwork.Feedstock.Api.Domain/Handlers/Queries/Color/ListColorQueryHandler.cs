using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Color;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Queries.Color
{
    public class ListColorQueryHandler : IRequestHandler<ListColorQueryRequest, IPagination<ColorQueryDto>>
    {
        private readonly IColorRepository _colorRepository;

        public ListColorQueryHandler(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<IPagination<ColorQueryDto>> Handle(ListColorQueryRequest request, CancellationToken cancellationToken)
        {
            var pagination = await _colorRepository.ListAsync(request.Filter, request.OrderBy, request.Page, request.QtyPerPage);

            if (pagination.IsEmpty())
            {
                return Pagination<ColorQueryDto>.Empty;
            }

            var colorDtos = ColorMapper.Map(pagination.Entities);

            var paginationDto = Pagination<ColorQueryDto>.New(colorDtos, pagination.TotalPages, pagination.ItemsPerPage, pagination.CurrentPage);

            return paginationDto;
        }
    }
}
