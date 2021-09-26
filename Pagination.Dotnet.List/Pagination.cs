using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagination.Dotnet.List
{
	public class Pagination<T>
	{
		public Pagination(IList<T> results, long totalItems, int page = 1, int limit = 10)
		{

			var startIndex = (page - 1) * limit;
			var endIndex = page * limit;
			var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)limit);

			TotalItems = totalItems;
			CurrentPage = page;
			Results = results ?? new List<T>();

			if (startIndex > 0)
			{
				PreviousPage = page - 1;
			}
			if (endIndex < totalItems)
			{
				NextPage = page + 1;
			}

			Pages = totalPages > 0 ? Enumerable.Range(1, totalPages).ToList() : new List<int>();
		}

		public long TotalItems { get; private set; }
		public int CurrentPage { get; private set; }
		public int? NextPage { get; private set; }
		public int? PreviousPage { get; private set; }
		public IList<int> Pages { get; private set; }
		public IList<T> Results { get; private set; }
	}


}
