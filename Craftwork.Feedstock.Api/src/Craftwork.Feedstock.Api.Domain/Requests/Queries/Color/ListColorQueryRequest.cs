using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Craftwork.Feedstock.Api.Domain.Requests.Queries.Color
{
    public class ListColorQueryRequest : IRequest<IPagination<ColorQueryDto>>
    {
        public Expression<Func<Entities.Color, bool>> Filter { get; private set; }

        public string OrderBy { get; private set; }

        public int Page { get; private set; }

        public int QtyPerPage { get; private set; }

        private ListColorQueryRequest() { }

        public static ListColorQueryRequest New(string filter, string orderBy = null,
            int page = 0, int qtyPerPage = 0)
        {
            return new ListColorQueryRequest()
            {
                Filter = CreateFilter(filter),
                OrderBy = orderBy,
                Page = page,
                QtyPerPage = qtyPerPage
            };
        }

        private static Expression<Func<Entities.Color, bool>> CreateFilter(string text)
        {
            return x => EF.Functions.Like(x.Name, $"{text}%");
        }
    }
}
