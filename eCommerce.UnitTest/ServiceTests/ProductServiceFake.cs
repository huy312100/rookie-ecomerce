//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using eCommerce.BackendApi.Interfaces;
//using eCommerce.Shared.ViewModels.Categories;
//using eCommerce.Shared.ViewModels.Common;
//using eCommerce.Shared.ViewModels.Products;

//namespace eCommerce.UnitTest.ServiceTests
//{
//	public class ProductServiceFake : IProductService
//	{
//		private readonly List<ProductVM> _product;
//		public ProductServiceFake()
//		{
//			_product = new List<ProductVM>()
//			{
//				new ProductVM()
//				{
//					Id = 1,
//					Name="Iphone 13",
//					Description="New design from apple",
//					Price=25000000,
//					StarAverage=3,
//					CreatedDate="2022-03-12 18:38:26.0000000",
//					UpdatedDate=null,
//					Category=new CategoryVM()
//					{
//						Id=1,
//						Name="Smartphone",
//						Description="Category of smartphone",
//						ImageUrl=null,
//						CreatedDate="2022-03-12 18:38:26.0000000",
//						ParentId=null,
//					},
//					Images= new List<ProductImageVM>()
//					{
//						[
//							{
//								Id = 1,
//								ImageUrl = "https://rooter.lk/storage/phones/13-pro-max/13-pro-max-blue/iphone-13-pro-max-5g-smarttelefon-128gb-sierrabla-pdp-zoom-3000.jpeg",
//								IsThumbnail = true
//							},
//							{
//								Id = 2,
//								ImageUrl = ,
//								IsThumbnail = true
//							}
//						]
//                    }
//				},

//				new ProductVM()
//				{
//					Id = 2,
//					Name="Macbook M1 2020",
//					Description="New laptop from apple",
//					Price=45000000,
//					StarAverage=5,
//					CreatedDate="2022-03-12 18:38:26.0000000",
//					UpdatedDate=null,
//					Category=new CategoryVM()
//					{
//						Id=2,
//						Name="Computer",
//						Description="Category of computer",
//						ImageUrl=null,
//						CreatedDate="2022-03-12 18:38:26.0000000",
//						ParentId=null,
//					},
//					Images= new List<ProductImageVM>()
//					{
//						[
//							{
//								Id = 3,
//								ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcS8GaucaJf_trFfUKcDdH6JR8J7wetymyyThD-dbfzvJ7MCR3gLgp0gA7e0wuECESAC4EEdqsP6_g&usqp=CAc",
//								IsThumbnail = true
//							}
//						]
//                    }
//				};

//			}


//		public async Task<List<ProductVM>> GetAllProducts()
//        {
//			return await _prodduct;
//        }

//		public async Task<ProductVM> GetProductById(int id)
//        {
//			return await _product.Where(a => a.Id == id)
//			.FirstOrDefault();
//		}

//		//public Task<ProductImageVM> GetImageById(int id);
//		//Task<PagedResult<ProductVM>> GetProductPaging(PagingRequest req);
//		//Task<PagedResult<ProductVM>> GetProductByCategory(PagingRequest req, int categoryId);
//		public async Task<int> CreateProduct(ProductCreateRequest req)
//        {

//        }
//		Task<int> DeleteProduct(int productId);
//	}
//	}
//}

