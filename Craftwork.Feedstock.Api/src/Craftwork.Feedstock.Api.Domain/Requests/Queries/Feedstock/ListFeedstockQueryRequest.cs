using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Dtos.Feedstock;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Craftwork.Feedstock.Api.Domain.Requests.Queries.Feedstock
{
    public class ListFeedstockQueryRequest : IRequest<IPagination<FeedstockQueryDto>>
    {
        public Expression<Func<Entities.Feedstock, bool>> Filter { get; private set; }

        public string OrderBy { get; private set; }

        public int Page { get; private set; }

        public int QtyPerPage { get; private set; }

        private ListFeedstockQueryRequest() { }

        public static ListFeedstockQueryRequest New(string filter, string orderBy = null,
            int page = 0, int qtyPerPage = 0)
        {
            return new ListFeedstockQueryRequest()
            {
                Filter = CreateFilter(filter),
                OrderBy = orderBy,
                Page = page,
                QtyPerPage = qtyPerPage
            };
        }

        private static Expression<Func<Entities.Feedstock, bool>> CreateFilter(string text)
        {
            return x => EF.Functions.Like(x.Name, $"{text}%");
        }
    }
}
