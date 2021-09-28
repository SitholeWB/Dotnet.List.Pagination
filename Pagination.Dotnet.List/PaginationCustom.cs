using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagination.Dotnet.List
{
	public class PaginationCustom<Tsource, Tdestination>
	{
		public PaginationCustom(IEnumerable<Tsource> results, long totalItems, Func<Tsource, Tdestination> convertTsourceToTdestinationMethod, int page = 1, int limit = 10)
		{
			var startIndex = (page - 1) * limit;
			var endIndex = page * limit;
			var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)limit);

			TotalItems = totalItems;
			CurrentPage = page;
			Results = results?.Select(a => convertTsourceToTdestinationMethod(a)) ?? Enumerable.Empty<Tdestination>();

			if (startIndex > 0)
			{
				PreviousPage = page - 1;
			}
			if (endIndex < totalItems)
			{
				NextPage = page + 1;
			}

			Pages = totalPages > 0 ? Enumerable.Range(1, totalPages) : Enumerable.Empty<int>();
		}

		public long TotalItems { get; private set; }
		public int CurrentPage { get; private set; }
		public int? NextPage { get; private set; }
		public int? PreviousPage { get; private set; }
		public IEnumerable<int> Pages { get; private set; }
		public IEnumerable<Tdestination> Results { get; private set; }
	}
}