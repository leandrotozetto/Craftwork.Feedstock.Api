using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Feedstock;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Queries.Feedstock
{
    public class ListFeedstockQueryHandler : IRequestHandler<ListFeedstockQueryRequest, IPagination<FeedstockQueryDto>>
    {
        private readonly IFeedstockRepository _colorRepository;

        public ListFeedstockQueryHandler(IFeedstockRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<IPagination<FeedstockQueryDto>> Handle(ListFeedstockQueryRequest request, CancellationToken cancellationToken)
        {
            var pagination = await _colorRepository.ListAsync(request.Filter, request.OrderBy, request.Page, request.QtyPerPage);

            if (pagination.IsEmpty())
            {
                return Pagination<FeedstockQueryDto>.Empty;
            }

            var colorDtos = FeedstockMapper.Map(pagination.Entities);

            var paginationDto = Pagination<FeedstockQueryDto>.New(colorDtos, pagination.TotalPages, pagination.ItemsPerPage, pagination.CurrentPage);

            return paginationDto;
        }
    }
}
