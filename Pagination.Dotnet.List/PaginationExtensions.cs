using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pagination.Dotnet.List
{
	public static class PaginationExtensions
	{
		public static async Task<Pagination<TSource>> AsPaginationAsync<TSource>(this IQueryable<TSource> source, int page, int limit, string sortColumn = null, bool orderByDescending = false)
		{
			var totalItems = await source.CountAsync();
			if (!string.IsNullOrEmpty(sortColumn))
			{
				source = orderByDescending ? source.OrderByDescending(p => EF.Property<object>(p, sortColumn)) : source.OrderBy(p => EF.Property<object>(p, sortColumn));
			}
			var results = source.Skip((page - 1) * limit).Take(limit);

			return new Pagination<TSource>(await results.ToListAsync(), totalItems, page, limit);
		}

		public static async Task<Pagination<TSource>> AsPaginationAsync<TSource>(this DbSet<TSource> source, int page, int limit, Expression<Func<TSource, bool>> expression, string sortColumn = null, bool orderByDescending = false) where TSource : class
		{
			var totalItems = await source.Where(expression).CountAsync();
			var results = Enumerable.Empty<TSource>();
			if (!string.IsNullOrEmpty(sortColumn))
			{
				results = await (orderByDescending ? source.OrderByDescending(p => EF.Property<object>(p, sortColumn)) : source.OrderBy(p => EF.Property<object>(p, sortColumn))).ToListAsync();
			}
			else
			{
				results = await source.Where(expression).Skip((page - 1) * limit).Take(limit).ToListAsync();
			}
			return new Pagination<TSource>(results, totalItems, page, limit);
		}

		public static async Task<PaginationAuto<TSource, Tdestination>> AsPaginationAsync<TSource, Tdestination>(this IQueryable<TSource> source, int page, int limit, Func<TSource, Tdestination> convertTsourceToTdestinationMethod, string sortColumn = null, bool orderByDescending = false)
		{
			var totalItems = await source.CountAsync();
			if (!string.IsNullOrEmpty(sortColumn))
			{
				source = orderByDescending ? source.OrderByDescending(p => EF.Property<object>(p, sortColumn)) : source.OrderBy(p => EF.Property<object>(p, sortColumn));
			}
			var results = source.Skip((page - 1) * limit).Take(limit);

			return new PaginationAuto<TSource, Tdestination>(await results.ToListAsync(), totalItems, convertTsourceToTdestinationMethod, page, limit);
		}

		public static async Task<PaginationAuto<TSource, Tdestination>> AsPaginationAsync<TSource, Tdestination>(this DbSet<TSource> source, int page, int limit, Expression<Func<TSource, bool>> expression, Func<TSource, Tdestination> convertTsourceToTdestinationMethod, string sortColumn = null, bool orderByDescending = false) where TSource : class
		{
			var totalItems = await source.Where(expression).CountAsync();
			var results = Enumerable.Empty<TSource>();
			if (!string.IsNullOrEmpty(sortColumn))
			{
				results = await (orderByDescending ? source.OrderByDescending(p => EF.Property<object>(p, sortColumn)) : source.OrderBy(p => EF.Property<object>(p, sortColumn))).ToListAsync();
			}
			else
			{
				results = await source.Where(expression).Skip((page - 1) * limit).Take(limit).ToListAsync();
			}
			return new PaginationAuto<TSource, Tdestination>(results, totalItems, convertTsourceToTdestinationMethod, page, limit);
		}
	}
}