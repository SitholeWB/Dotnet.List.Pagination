﻿using System;
using System.Runtime.Serialization;

namespace Pagination.Dotnet.List
{
	[Serializable]
	public sealed class PaginationException : Exception
	{
		public PaginationException(string message) : base(message)
		{
		}

		private PaginationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}