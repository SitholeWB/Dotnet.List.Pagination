using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pagination.Dotnet.List.Tests
{
	public class UsersDbContext : DbContext
	{
		public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}