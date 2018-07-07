using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Extensions
{
    public static class PagedHelper
    {
        public static async Task<PageInfo<TReturn>> CreatePagedResultsAsync<T, TReturn>(IQueryable<T> query, int page, int pageSize, Func<T, TReturn> mapper)
        {
            var projection = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var totalNumberOfRecords = await query.CountAsync();

            var results = (await projection.Distinct().ToListAsync()).Select(mapper);

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            var pageInfo = new PageInfo<TReturn>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPageCount,
                Results = results
            };

            return pageInfo;
        }

        public static PageInfo<TReturn> CreatePagedResults<T, TReturn>(
            IEnumerable<T> response,
            int page,
            int pageSize,
            Func<T, TReturn> mapper)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = response
                .Skip(skipAmount)
                .Take(pageSize);

            var totalNumberOfRecords = response.Count();
            var results = projection.Select(mapper);

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PageInfo<TReturn>
            {
                Results = results,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPageCount
            };
        }
    }
}
