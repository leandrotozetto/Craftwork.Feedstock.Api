using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Dtos.Measure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Craftwork.Feedstock.Api.Domain.Requests.Queries.Measure
{
    public class ListMeasureQueryRequest : IRequest<IPagination<MeasureQueryDto>>
    {
        public Expression<Func<Entities.Measure, bool>> Filter { get; private set; }

        public string OrderBy { get; private set; }

        public int Page { get; private set; }

        public int QtyPerPage { get; private set; }

        private ListMeasureQueryRequest() { }

        public static ListMeasureQueryRequest New(string filter, string orderBy = null,
            int page = 0, int qtyPerPage = 0)
        {
            return new ListMeasureQueryRequest()
            {
                Filter = CreateFilter(filter),
                OrderBy = orderBy,
                Page = page,
                QtyPerPage = qtyPerPage
            };
        }

        private static Expression<Func<Entities.Measure, bool>> CreateFilter(string text)
        {
            return x => EF.Functions.Like(x.Name, $"{text}%");
        }
    }
}
