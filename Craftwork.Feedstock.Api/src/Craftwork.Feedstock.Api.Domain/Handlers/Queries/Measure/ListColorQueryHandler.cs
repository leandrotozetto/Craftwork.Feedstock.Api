using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Queries.Measure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Domain.Handlers.Queries.Measure
{
    public class ListMeasureQueryHandler : IRequestHandler<ListMeasureQueryRequest, IPagination<MeasureQueryDto>>
    {
        private readonly IMeasureRepository _measureRepository;

        public ListMeasureQueryHandler(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }

        public async Task<IPagination<MeasureQueryDto>> Handle(ListMeasureQueryRequest request, CancellationToken cancellationToken)
        {
            var pagination = await _measureRepository.ListAsync(request.Filter, request.OrderBy, request.Page, request.QtyPerPage);

            if (pagination.IsEmpty())
            {
                return Pagination<MeasureQueryDto>.Empty;
            }

            var measureDtos = MeasureMapper.Map(pagination.Entities);

            var paginationDto = Pagination<MeasureQueryDto>.New(measureDtos, pagination.TotalPages, pagination.ItemsPerPage, pagination.CurrentPage);

            return paginationDto;
        }
    }
}
