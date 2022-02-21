using System;
using eCommerce.BackendApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.BackendApi.Data.EF
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,Guid>
	{
		
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Rating> Ratings { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
			base.OnModelCreating(builder);
			// Remove prefix AspNet of table
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}
		}
	}
}

