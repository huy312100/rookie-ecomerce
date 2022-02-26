using System;
using eCommerce.BackendApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.BackendApi.Data.EF
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,Guid>
	{
		
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
        {
			base.OnModelCreating(builder);

			//Add table name and using Fluent Api to create relationship
			builder.Entity<Category>().ToTable("Categories");
			builder.Entity<Category>().HasOne(prop => prop.Parent)
				.WithOne()
				.HasForeignKey<Category>(prop => prop.ParentId);

			builder.Entity<Order>().ToTable("Orders");
			builder.Entity<Order>().HasOne(prop => prop.User)
				.WithMany(prop => prop.Orders)
				.HasForeignKey(prop => prop.UserId);

			builder.Entity<OrderDetail>().ToTable("OrderDetails");
			builder.Entity<OrderDetail>().HasOne(prop => prop.Order)
				.WithMany(prop => prop.OrderDetails)
				.HasForeignKey(prop => prop.OrderId);
			builder.Entity<OrderDetail>().HasOne(prop => prop.Product)
				.WithMany(prop => prop.OrderDetails)
				.HasForeignKey(prop => prop.ProductId);

			builder.Entity<Product>().ToTable("Products");
			builder.Entity<Product>().HasOne(prop => prop.Category)
				.WithMany(prop => prop.Products)
				.HasForeignKey(prop => prop.CategoryId);

			builder.Entity<ProductImage>().ToTable("ProductImages");
			builder.Entity<ProductImage>().HasOne(prop => prop.Product)
				.WithMany(prop => prop.ProductImages)
				.HasForeignKey(prop => prop.ProductId);

			builder.Entity<Rating>().ToTable("Ratings");
			builder.Entity<Rating>().HasOne(prop => prop.Product)
				.WithMany(prop => prop.Ratings)
				.HasForeignKey(prop => prop.ProductId);
			builder.Entity<Rating>().HasOne(prop => prop.User)
				.WithMany(prop => prop.Ratings)
				.HasForeignKey(prop => prop.UserId);

			//Change table name identity entities
			builder.Entity<User>().ToTable("Users");
			builder.Entity<Role>().ToTable("Roles");
			builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
			builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles")
				.HasKey(prop => new { prop.UserId, prop.RoleId }); ;
			builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins")
				.HasKey(prop => prop.UserId);
			builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
			builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens")
				.HasKey(prop => prop.UserId);
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Rating> Ratings { get; set; }
	}
}

