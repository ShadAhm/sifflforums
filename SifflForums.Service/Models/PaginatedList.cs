using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Service.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static async Task<PaginatedList<TResult>> CreateAsync<TResult>(IQueryable<T> source, Func<T,TResult> selectExpression, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var itemsVm = items.Select(selectExpression).ToList();
            return new PaginatedList<TResult>(itemsVm, count, pageIndex, pageSize);
        }

        public PagedResult<T> ToPagedResult()
        {
            return new PagedResult<T>()
            {
                PageIndex = this.PageIndex,
                TotalPages = this.TotalPages,
                Results = this
            };
        }
    }

    public class PagedResult<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
